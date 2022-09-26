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
        /// The filter that is used when saving and opening files.
        /// </summary>
        private const string FILE_DIALOG_FILTER = $"LEGv8 Assembly Files (*{LegFile.EXTENSION})|*{LegFile.EXTENSION}|Text file (*.txt)|*.txt";

        private Dictionary<string, CoreInstruction> _coreInstructions = new Dictionary<string, CoreInstruction>();

        private LegFile _legFile;

        private bool _ignoreNextSetText = false;

        private RichTextBox _convertingBox;

        public MainForm()
        {
            InitializeComponent();

            _convertingBox = new RichTextBox();

            _legFile = new LegFile();
        }

        #region Instructions

        private void LoadCoreInstructions()
        {
            _coreInstructions.Clear();

            string[] lines = ProgramData.CoreInstructions.Split(Environment.NewLine);

            string line;

            string[] args;
            string[] ops;

            string mnemonic;

            CoreInstruction c;

            //skip the first line, which is just the headers
            for (int i = 1; i < lines.Length; i++)
            {
                line = lines[i];

                //ignore blank lines
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                //split up by arguments
                args = line.Split(',');

                mnemonic = args[1];

                if (args.Length == 4)//name, mnemonic, format, opcode
                {
                    ops = args[3].Split('-');

                    c = new CoreInstruction(args[0], Enum.Parse<InstructionMnemonic>(mnemonic), Enum.Parse<InstructionFormat>(args[2]), int.Parse(ops[0], System.Globalization.NumberStyles.HexNumber), int.Parse(ops.Length == 1 ? ops[0] : ops[1], System.Globalization.NumberStyles.HexNumber));
                }
                else if (args.Length == 5)//name, mnemonic, format, opcode, shamt
                {
                    ops = args[3].Split('-');

                    c = new CoreInstruction(args[0], Enum.Parse<InstructionMnemonic>(mnemonic), Enum.Parse<InstructionFormat>(args[2]), int.Parse(ops[0], System.Globalization.NumberStyles.HexNumber), int.Parse(ops.Length == 1 ? ops[0] : ops[1], System.Globalization.NumberStyles.HexNumber), int.Parse(args[4], System.Globalization.NumberStyles.HexNumber));
                }
                else
                {
                    //ignore this line, it is bad
                    throw new ArgumentException($"The line \"{line}\"({i + 1}) has an invalid number of arguments ({args.Length})!");
                }

                //add core instruction to the dictionary
                _coreInstructions.Add(mnemonic, c);
            }

            //now that they have been loaded, send that to the formatter for later use
            RtfLEGv8Formatter.SetKeywords(_coreInstructions.Keys.ToArray());
        }

        private Instruction ParseInstruction(Line line, Dictionary<string, int> headers)
        {
            int[] args = line.Args.Select(a => ParseArgument(a, headers)).ToArray();

            int arg0 = args.Length > 0 ? args[0] : 0;
            int arg1 = args.Length > 1 ? args[1] : 0;
            int arg2 = args.Length > 2 ? args[2] : 0;

            //find the core instruction
            if (_coreInstructions.TryGetValue(line.Header, out CoreInstruction? ci))
            {
                switch (ci.Format)
                {
                    case InstructionFormat.R:
                        return new RInstruction(ci, ci.OpCodeStart, arg2, arg2, arg1, arg0);
                    case InstructionFormat.I:
                        return new IInstruction(ci, ci.OpCodeStart, arg2, arg1, arg0);
                    case InstructionFormat.D:
                        return new DInstruction(ci, ci.OpCodeStart, arg2, 0, arg1, arg0);
                    case InstructionFormat.B:
                        return new BInstruction(ci, ci.OpCodeStart, arg0);
                    case InstructionFormat.CB:
                        return new CBInstruction(ci, ci.OpCodeStart, arg1, arg0);
                        //case InstructionFormat.IM:
                        //return new IMInstruction(ci, ci.OpCodeStart, )
                }
            }

            return new EmptyInstruction();
        }

        private int ParseArgument(string arg, Dictionary<string, int> headers)
        {
            //determine what to do based on the starting char
            switch (arg[0])
            {
                case 'X'://register
                    {
                        if (arg == "XZR")
                        {
                            return 31;//zero register
                        }
                        else
                        {
                            //another register, parse the number next to the X
                            return int.Parse(arg.Substring(1));
                        }
                    }
                case '#'://number
                    return int.Parse(arg.Substring(1));
                default:
                    //check special cases for registers
                    switch (arg)
                    {
                        case "IP0": return 16;
                        case "IP1": return 17;
                        case "SP": return 28;//stack pointer
                        case "FP": return 29;//frame pointer
                        case "LR": return 30;//return address
                    }

                    int value;
                    if (int.TryParse(arg, out value))//check if just a raw number
                    {
                        //return that number
                        return value;
                    }
                    else if (headers.TryGetValue(arg, out value))//check if a header name
                    {
                        //use the line the header corresponds to
                        return value;
                    }

                    throw new Exception($"Does not recognize \"{arg}\".");
            }
        }

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
                line = new Line(strLine);

                //if no args, must be a header
                if (line.Args.Length == 0)
                {
                    if (line.Header.EndsWith(Constants.HEADER_POSTFIX))
                    {
                        headers.Add(line.Header.TrimEnd(Constants.HEADER_POSTFIX), instructionLines.Count);
                    }
                }
                else
                {
                    //add to list of lines
                    instructionLines.Add(line);
                }
            }

            //got the lines organized, now create instructions from them
            Instruction[] instructions = new Instruction[instructionLines.Count];

            for (int i = 0; i < instructionLines.Count; i++)
            {
                instructions[i] = ParseInstruction(instructionLines[i], headers);
            }

            return instructions;
        }

        private void Run()
        {
            //get all instructions
            Instruction[] instructions = GetInstructions(GetText(true).Split('\n'));

            SimulationForm form = new SimulationForm(new Simulation(instructions), _legFile.Name);

            form.Show();
        }

        #endregion

        #region Undo/Redo

        private void Undo()
        {
            if (MainRichTextBox.CanUndo)
            {
                MainRichTextBox.Undo();
            }
        }

        private void Redo()
        {
            //go to last redo, store this one on undo
            if (MainRichTextBox.CanRedo)
            {
                MainRichTextBox.Redo();
            }
        }

        #endregion

        #region Loading and Saving

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

        private void LoadFile(LegFile file)
        {
            //set title
            SetFormTitle(file.Name);

            //set text
            SetText(file.Text, false);

            //set reference
            _legFile = file;
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
        }

        private void SetFilePath(LegFile file, string path)
        {
            //set the path on the file
            file.FileName = path;

            //set the title
            SetFormTitle(file.Name);
        }

        #endregion

        #region Form

        private void SetFormTitle(string text)
        {
            Text = $"{FORM_TEXT} - {text}";
        }

        private void SetText(string text, bool rtf)
        {
            //only set if there is something to set
            if (string.IsNullOrEmpty(text))
            {
                return;
            }


            //set text
            if (rtf)
            {
                //replace text with new text

                //get selected position relative to end
                int index = MainRichTextBox.Text.Length - 1 - MainRichTextBox.SelectionStart;

                //set text, do not trigger infinite loop
                _ignoreNextSetText = true;
                MainRichTextBox.Rtf = text;
                _ignoreNextSetText = false;

                //set selected back to where we were
                MainRichTextBox.SelectionStart = MainRichTextBox.Text.Length - 1 - index;
                MainRichTextBox.SelectionLength = 0;
            }
            else
            {
                //not RTF, yet
                //format text
                SetText(RtfLEGv8Formatter.FormatString(text), true);
            }
        }

        private string GetText(bool rtf)
        {
            if(rtf)
            {
                _convertingBox.Rtf = MainRichTextBox.Rtf.Replace(@"\line", @"\par");
                return _convertingBox.Text;
            } else
            {
                return MainRichTextBox.Text;
            }
        }

        #endregion

        #region Form Events

        #region Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            //load program data
            LoadCoreInstructions();

            string lastFilePath = FileSettings.Default.LastFilePath;

            //if the last file does not exist
            if (string.IsNullOrEmpty(lastFilePath) || !File.Exists(lastFilePath))
            {
                //load user data
                LoadFile(new LegFile());

                //test
                MainRichTextBox.Lines = _coreInstructions.Values.Select(c => c.ToString()).ToArray();
            }
            else
            {
                //load last user data file path
                LoadFile(new LegFile(lastFilePath));
            }
        }

        #endregion

        private void MainRichTextBox_TextChanged(object sender, EventArgs e)
        {
            if(_ignoreNextSetText)
            {
                //_ignoreNextSetText = false;
                return;
            }

            //when the text is changed, reformat
            SetText(GetText(true), false);
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

        #region Run

        private void MainMenuStrip_Run_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Run();
        }

        #endregion

        #endregion

        #endregion
    }
}