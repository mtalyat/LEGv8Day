using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace LEGv8Day
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// The constant text that is displayed before the name of the file for the title of the form.
        /// </summary>
        private const string FORM_TEXT = "LEGv8 Day";

        /// <summary>
        /// The constant name of the file for themes.
        /// </summary>
        private const string THEMES_PATH = "Themes.txt";

        /// <summary>
        /// The filter that is used when saving and opening files.
        /// </summary>
        private const string FILE_DIALOG_FILTER = $"LEGv8 Assembly Files (*{LegFile.EXTENSION})|*{LegFile.EXTENSION}|Text file (*.txt)|*.txt";

        private const string URL_DOCUMENTATION = @"https://github.com/mtalyat/LEGv8Day/wiki";

        private const string URL_REPOSITORY = @"https://github.com/mtalyat/LEGv8Day";

        private LegFile _legFile;

        private List<Theme> _themes;

        private bool _ignoreNextSetText = false;

        private string _title = "";

        private Stack<string> _undos = new Stack<string>();
        private bool canUndo => _undos.Any();
        private Stack<string> _redos = new Stack<string>();
        private bool canRedo => _redos.Any();
        private string _rtf = "";
        private string _lastSavedText = "";

        private RichTextBox _convertingBox;

        public MainForm()
        {
            InitializeComponent();

            _themes = Data.Load<List<Theme>>(THEMES_PATH) ?? new List<Theme>(new Theme[1] { Theme.Default });
            //always ensure the 1st is the actual default, not modified outside of this environment
            _themes[0] = Theme.Default;
            FormSettings.Default.ActiveThemeIndex = Math.Min(FormSettings.Default.ActiveThemeIndex, _themes.Count - 1);

            _convertingBox = new RichTextBox();

            _legFile = new LegFile();

            RefreshForm();
        }

        #region Instructions

        #endregion

        #region Running

        private Instruction[] GetInstructions(string[] strLines)
        {
            Dictionary<string, int> headers = new Dictionary<string, int>();
            List<Line> instructionLines = new List<Line>();

            string strLine;
            Line line;

            //run through once, remove all comments, gather headers
            for (int i = 0; i < strLines.Length; i++)
            {
                strLine = strLines[i];

                //if line is empty, skip it
                if (string.IsNullOrWhiteSpace(strLine))
                {
                    continue;
                }

                //remove comment
                int commentIndex = strLine.IndexOf(Constants.COMMENT_SINGLE);
                if (commentIndex >= 0)
                {
                    strLine = strLine.Substring(0, commentIndex);
                }

                //turn into a line
                line = new Line(strLine, i + 1);

                //if no args and not a mnemonic, must be a header
                if (line.IsInstruction())
                {
                    //add to list of lines
                    instructionLines.Add(line);
                }
                else
                {
                    if (line.Label.EndsWith(Constants.HEADER_POSTFIX))
                    {
                        headers.Add(line.Label.TrimEnd(Constants.HEADER_POSTFIX), instructionLines.Count);
                    }
                }
            }

            //got the lines organized, now create instructions from them
            Instruction[] instructions = new Instruction[instructionLines.Count];

            for (int i = 0; i < instructionLines.Count; i++)
            {
                instructions[i] = Parse.ParseInstruction(instructionLines[i], headers);
            }

            return instructions;
        }

        private void Run()
        {
            //get all instructions
            Instruction[] instructions = GetInstructions(GetText(true).Split('\n'));

            //SimulationForm form = new SimulationForm(this, new Simulation(instructions), _legFile.Name);
            RunForm form = new RunForm(this, new Emulation(instructions), _legFile.Name);

            form.Show();
        }

        #endregion

        #region Undo/Redo

        private void Undo()
        {
            if (canUndo)
            {
                //push to redos
                _redos.Push(GetText(false));

                //pull from undos
                SetText(_undos.Pop(), true);
            }
        }

        private void Redo()
        {
            //go to last redo, store this one on undo
            if (canRedo)
            {
                //push to undos
                _undos.Push(GetText(false));

                //pull from redos
                SetText(_redos.Pop(), true);
            }
        }

        #endregion

        #region Loading and Saving

        private void SetFilePath(LegFile file, string path)
        {
            //set the path on the file
            file.FileName = path;

            //set the title
            SetFormTitle(file.Name);
        }

        #region Load

        private bool LoadDialog(out string path)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select a LEGv8 Assembly file.";
            ofd.Filter = FILE_DIALOG_FILTER;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                //file selected
                path = ofd.FileName;
                return true;
            }
            else
            {
                //no file selected
                path = string.Empty;
                return false;
            }
        }

        private void LoadFile(LegFile file)
        {
            //set title
            SetFormTitle(file.Name);

            //set text
            SetText(file.Text, false);

            //set reference
            _legFile = file;

            //up to date
            _lastSavedText = file.Text;
            SetSaveState(true);
        }

        #endregion

        #region Save

        private bool SaveDialog(out string path)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Select a location to save.";
            sfd.Filter = FILE_DIALOG_FILTER;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //location chosen
                path = sfd.FileName;
                return true;
            }
            else
            {
                //user canceled
                path = string.Empty;
                return false;
            }
        }

        private void SaveFile(LegFile file)
        {
            //get text
            string text = GetText(true);

            //set to file object
            file.Text = text;

            //save
            file.Save();

            //log last file path
            FileSettings.Default.LastFilePath = file.FileName;
            FileSettings.Default.Save();

            //let the user know
            _lastSavedText = text;
            SetSaveState(true);
        }

        #endregion

        #endregion

        #region Form

        private void SetFormTitle(string text)
        {
            _title = $"{FORM_TEXT} - {text}";
            Text = _title;
        }

        private void SetSaveState(bool upToDate)
        {
            Text = $"{_title}{(upToDate ? "" : "*")}";
        }

        private void SetText(string text, bool rtf)
        {
            //set text
            if (rtf)
            {
                //replace text with new text

                //get selected position relative to end
                int index = Assembly_RichTextBox.Text.Length - 1 - Assembly_RichTextBox.SelectionStart;

                //set text, do not trigger infinite loop
                _ignoreNextSetText = true;
                Assembly_RichTextBox.Rtf = text;
                _ignoreNextSetText = false;

                _rtf = text;

                //set selected back to where we were
                Assembly_RichTextBox.SelectionStart = Math.Max(Assembly_RichTextBox.Text.Length - 1 - index, 0);
                Assembly_RichTextBox.SelectionLength = 0;
            }
            else
            {
                //not RTF, yet
                //format text
                SetText(RtfLEGv8Formatter.FormatString(text), true);
            }
        }

        private string GetText(bool plainText)
        {
            if(plainText)
            {
                _convertingBox.Rtf = Assembly_RichTextBox.Rtf.Replace(@"\line", @"\par");
                return _convertingBox.Text;
            } else
            {
                return Assembly_RichTextBox.Rtf;
            }
        }

        private void RefreshText()
        {
            SetText(GetText(true), false);
        }

        public Theme GetActiveTheme()
        {
            return FormSettings.Default.ActiveThemeIndex < _themes.Count ? _themes[FormSettings.Default.ActiveThemeIndex] : Theme.Default;
        }

        public void RefreshForm()
        {
            //refresh theme
            Theme active = GetActiveTheme();

            RtfLEGv8Formatter.SetTheme(active);

            RefreshText();

            BackColor = active.PrimaryColor;

            Assembly_RichTextBox.BackColor = active.SecondaryColor;
        }

        private void LoadAllData()
        {
            //load program data
            Parse.LoadCoreInstructions();

            string lastFilePath = FileSettings.Default.LastFilePath;

            //if the last file does not exist
            if (string.IsNullOrEmpty(lastFilePath) || !File.Exists(lastFilePath))
            {
                //load user data
                LoadFile(new LegFile());
            }
            else
            {
                //load last user data file path
                LoadFile(new LegFile(lastFilePath));
            }

            //auto dump
            MainMenuStrip_AutoDump_ToolStripMenuItem.Checked = FormSettings.Default.AutoDump;
        }

        private void SaveAllData()
        {
            FormSettings.Default.Save();
            FileSettings.Default.Save();

            Data.Save(THEMES_PATH, _themes);
        }

        private void OpenURL(string link)
        {
            Process.Start("explorer", link);
        }

        #endregion

        #region Form Events

        #region Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAllData();
        }

        #endregion

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if(_ignoreNextSetText)
            {
                //_ignoreNextSetText = false;
                return;
            }

            //log last text stored
            _undos.Push(_rtf);

            //reformat
            string text = GetText(true);

            SetText(text, false);

            SetSaveState(text == _lastSavedText);
        }

        #region Main Menu Strip

        #region File

        private void MainMenuStrip_New_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFile(new LegFile());
        }

        private void MainMenuStrip_Open_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoadDialog(out string path))
            {
                LoadFile(new LegFile(path));
            }
        }

        private void MainMenuStrip_Save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_legFile.FileName))
            {
                //no file path to save to, so save as
                MainMenuStrip_SaveAs_ToolStripMenuItem_Click(sender, e);
                return;
            }

            SaveFile(_legFile);
        }

        private void MainMenuStrip_SaveAs_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveDialog(out string path))
            {
                //set new path
                SetFilePath(_legFile, path);

                SaveFile(_legFile);
            }
        }

        private void MainMenuStrip_Refresh_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_legFile.FileName))
            {
                LoadFile(new LegFile(_legFile.FileName));
            }
        }

        #endregion

        #region Edit

        private void MainMenuStrip_Undo_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void MainMenuStrip_Redo_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Redo();
        }

        #endregion

        #region View

        private void MainMenuStrip_Theme_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThemeForm form = new ThemeForm(_themes, this);

            form.Show();
        }

        #endregion

        #region Run

        private void MainMenuStrip_Run_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void MainMenuStrip_AutoDump_ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormSettings.Default.AutoDump = MainMenuStrip_AutoDump_ToolStripMenuItem.Checked;
        }

        #endregion

        #region Help

        private void MainMenuStrip_Documentation_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL(URL_DOCUMENTATION);
        }

        private void MainMenuStrip_Repository_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL(URL_REPOSITORY);
        }

        #endregion

        #endregion

        #endregion
    }
}