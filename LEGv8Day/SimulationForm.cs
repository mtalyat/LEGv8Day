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
    public partial class SimulationForm : Form
    {
        private const string FORM_TEXT = "LEGv8 Day Simulation";

        private readonly MainForm _mainForm;

        private readonly Simulation _simulation;

        public SimulationForm(MainForm mainForm, Simulation simulation, string name)
        {
            _mainForm = mainForm;

            _simulation = simulation;

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
            _simulation.Run();

            ExecutionTime_Label.Text = $"Execution Time: {_simulation.ExecutionTime}ms";

            SimulationRichTextBox.Lines = _simulation.Dump();
        }

        #endregion

        #endregion
    }
}
