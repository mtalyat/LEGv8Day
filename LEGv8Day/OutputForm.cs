using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

            BackColor = theme.PrimaryColor;
            ForeColor = theme.PrimaryColor.GetTextColor();
            SimulationRichTextBox.BackColor = theme.SecondaryColor;
            SimulationRichTextBox.ForeColor = theme.SecondaryColor.GetTextColor();
        }

        #region Form Events

        #region Form

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            ExecutionTime_Label.Text = $"Execution Time: {_emulation.ExecutionTime}ms";

            SimulationRichTextBox.Lines = _emulation.GetOutput();
        }

        #endregion

        #endregion
    }
}
