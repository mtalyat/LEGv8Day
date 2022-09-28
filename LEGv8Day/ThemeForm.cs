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
    public partial class ThemeForm : Form
    {
        private readonly MainForm _mainForm;

        private readonly List<Theme> _themes;

        public ThemeForm(List<Theme> themes, MainForm mainForm)
        {
            _mainForm = mainForm;

            _themes = themes;

            InitializeComponent();

            RefreshListBox();

            Themes_ListBox.SelectedIndex = FormSettings.Default.ActiveThemeIndex;
        }

        private void RefreshListBox()
        {
            Themes_ListBox.DataSource = null;
            Themes_ListBox.DataSource = _themes;
        }

        private void Add_Button_Click(object sender, EventArgs e)
        {
            _themes.Add(new Theme("New Theme"));

            RefreshListBox();

            //set index to new one
            Themes_ListBox.SelectedIndex = _themes.Count - 1;
        }

        private void Remove_Button_Click(object sender, EventArgs e)
        {
            int index = Themes_ListBox.SelectedIndex;

            if (index >= 0)
            {
                _themes.RemoveAt(index);

                RefreshListBox();

                Themes_ListBox.SelectedIndex = Math.Min(index, _themes.Count - 1);

                //just in case user deletes activated form
                _mainForm.RefreshForm();
            }
        }

        private void Activate_Button_Click(object sender, EventArgs e)
        {
            if (Themes_ListBox.SelectedIndex >= 0)
            {
                FormSettings.Default.ActiveThemeIndex = Themes_ListBox.SelectedIndex;

                OnSelectionChange();

                _mainForm.RefreshForm();
            }
        }

        private void Edit_Button_Click(object sender, EventArgs e)
        {
            if (Themes_ListBox.SelectedIndex >= 0)
            {
                EditThemeForm form = new EditThemeForm(_themes[Themes_ListBox.SelectedIndex], this);

                form.Show();
            }
        }

        public void RefreshForm()
        {
            RefreshListBox();

            _mainForm.RefreshForm();
        }

        private void OnSelectionChange()
        {
            int index = Themes_ListBox.SelectedIndex;

            if (index >= 0)
            {
                if (FormSettings.Default.ActiveThemeIndex != index)
                {
                    Activate_Button.Enabled = true;
                    Activate_Button.Text = "Activate";
                }
                else
                {
                    Activate_Button.Enabled = false;
                    Activate_Button.Text = "Active";
                }

                Edit_Button.Enabled = index > 0;//cannot edit default
                Remove_Button.Enabled = index > 0;//cannot remove default
            }
            else
            {
                Activate_Button.Enabled = false;
                Edit_Button.Enabled = false;
                Remove_Button.Enabled = false;
            }
        }

        private void Themes_ListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectionChange();
        }
    }
}
