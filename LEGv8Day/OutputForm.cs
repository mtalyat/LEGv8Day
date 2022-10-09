using LEGv8Day;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEGv8Day
{
    public partial class OutputForm : Form
    {
        private const string FORM_TEXT = "LEGv8 Day Emulation";

        private readonly MainForm _mainForm;

        private readonly Emulation _emulation;

        public OutputForm(MainForm mainForm, Emulation emulation, string name)
        {
            _mainForm = mainForm;

            _emulation = emulation;

            InitializeComponent();

            Text = $"{FORM_TEXT} - {name}";

            //set theme
            Theme theme = _mainForm.GetActiveTheme();

            this.SetTheme(theme.PrimaryColor);
            Output_RichTextBox.SetTheme(theme.SecondaryColor);
            StackTrace_RichTextBox.SetTheme(theme.SecondaryColor);
            OutputTabPage.SetTheme(theme.TertiaryColor);
            StackTraceTabPage.SetTheme(theme.TertiaryColor);
        }

        #region Form Events

        #region Form

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            //set label to name
            ExecutionTime_Label.Text = $"Execution Time: {_emulation.ExecutionTime}ms";

            //show stack trace
            StackTrace_RichTextBox.Lines = _emulation.GetStackTrace();

            //check for auto dump
            if (!_emulation.IsDumped && FormSettings.Default.AutoDump)
            {
                _emulation.Dump();
            }

            //show output
            Output_RichTextBox.Lines = _emulation.GetOutput();
        }

        #endregion

        #endregion
    }
}

public static class ControlExtensions
{
    public static void SetTheme(this Control control, Color foreground, Color background)
    {
        control.ForeColor = foreground;
        control.BackColor = background;
    }

    public static void SetTheme(this Control control, Color color)
    {
        control.BackColor = color;
        control.ForeColor = color.GetTextColor();
    }
}