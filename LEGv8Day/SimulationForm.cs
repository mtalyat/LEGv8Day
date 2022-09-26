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

        private readonly Simulation _simulation;

        public SimulationForm(Simulation simulation, string name)
        {
            _simulation = simulation;

            InitializeComponent();

            Text = $"{FORM_TEXT} - {name}";
        }

        #region Form Events

        #region Form

        private void SimulationForm_Load(object sender, EventArgs e)
        {
            _simulation.Run();

            SimulationRichTextBox.Lines = _simulation.Dump();
        }

        #endregion

        #endregion
    }
}
