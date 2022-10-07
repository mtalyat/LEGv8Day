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
    public partial class RunForm : Form
    {
        private readonly MainForm _mainForm;

        private readonly Simulation _simulation;

        private readonly string _simulationName;

        private object _lock = new object();

        public RunForm(MainForm mainForm, Simulation simulation, string name)
        {
            _mainForm = mainForm;
            _simulation = simulation;
            _simulationName = name;

            InitializeComponent();

            Text = $"Running {name}...";
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            lock(_lock)
            {
                _simulation.Stop();
            }
        }

        private async Task<Simulation> RunSimulation(Simulation simulation)
        {
            await Task.Run(() =>
            {
                simulation.Start();

                while (simulation.IsRunning)
                {
                    lock(_lock)
                    {
                        simulation.Step();
                    }

                    //report progress
                    //if (InvokeRequired)
                    //{
                    //    ElapsedTime_Label.Invoke(new Action(() =>
                    //    {
                    //        ElapsedTime_Label.Text = $"Elapsed time: {simulation.ExecutionTime / 1000.0f}s";
                    //    }));
                    //}
                    //else
                    //{
                    //    ElapsedTime_Label.Text = $"Elapsed time: {simulation.ExecutionTime / 1000.0f}s";
                    //}
                }

                return simulation;
            });

            return simulation;
        }

        private void RunForm_Load(object sender, EventArgs e)
        {
            
        }

        private async void RunForm_Shown(object sender, EventArgs e)
        {
            Simulation simulation;

            try
            {
                simulation = await RunSimulation(_simulation);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error running the emulation.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            

            //simulation completed
            if (simulation.IsCompleted)
            {
                //finished
                SimulationForm form = new SimulationForm(_mainForm, simulation, _simulationName);
                form.Show();
            }

            //close regardless of completed or canceled
            Close();
        }
    }
}
