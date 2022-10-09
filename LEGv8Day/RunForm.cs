using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LEGv8Day
{
    public partial class RunForm : Form
    {
        private readonly MainForm _mainForm;

        private readonly Emulation _emulation;

        private readonly string _simulationName;

        private object _lock;

        public RunForm(MainForm mainForm, Emulation emulation, string name)
        {
            _mainForm = mainForm;
            _emulation = emulation;
            _simulationName = name;

            _lock = new object();

            InitializeComponent();

            Text = $"Running {name}...";

            //set theme
            Theme theme = _mainForm.GetActiveTheme();

            this.SetTheme(theme.PrimaryColor);
            Cancel_Button.SetTheme(theme.SecondaryColor);
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            lock(_lock)
            {
                _emulation.Cancel();
            }
        }

        private Task RunEmulation()
        {
            return Task.Run(() =>
            {
                _emulation.Start();

                while (_emulation.IsRunning)
                {
                    lock (_lock)
                    {
                        _emulation.Step();
                    }
                }
            });
        }

        private void RunForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void RunForm_Shown(object sender, EventArgs e)
        {
            await RunEmulation();

            if (!_emulation.IsRunning)
            {
                //finished
                OutputForm form = new OutputForm(_mainForm, _emulation, _simulationName);

                form.Show();
            }

            //close regardless of completed or canceled or whatever
            Close();
        }
    }
}