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
    public partial class EditThemeForm : Form
    {
        private const string FORM_TITLE = "Edit Theme";

        private readonly Theme _theme;

        private readonly ThemeForm _themeForm;

        public EditThemeForm(Theme theme, ThemeForm themeForm)
        {
            _theme = theme;
            _themeForm = themeForm;

            InitializeComponent();

            Name_TextBox.Text = theme.Name;

            RefreshForm();
        }

        private void SetTitle(string name)
        {
            Text = $"{FORM_TITLE} - {name}";
        }

        public void RefreshForm()
        {
            SetTitle(_theme.Name);

            SetButtonColor(PrimaryColor_Button, _theme.PrimaryColor);
            SetButtonColor(SecondaryColor_Button, _theme.SecondaryColor);
            SetButtonColor(TertiaryColor_Button, _theme.TertiaryColor);

            SetButtonTextColor(TextColor_Button, _theme.TextColor);
            SetButtonTextColor(KeywordColor_Button, _theme.KeywordColor);
            SetButtonTextColor(RegisterColor_Button, _theme.RegisterColor);
            SetButtonTextColor(NumberColor_Button, _theme.NumberColor);
            SetButtonTextColor(CommentColor_Button, _theme.CommentColor);
            SetButtonTextColor(LabelColor_Button, _theme.LabelColor);

            _themeForm.RefreshForm();
        }

        private void SetButtonColor(Button button, Color color)
        {
            button.BackColor = color;

            //set text color to white or black based on darkness of given background color
            button.ForeColor = color.GetTextColor();
        }

        private void SetButtonTextColor(Button button, Color color)
        {
            button.ForeColor = color;

            button.BackColor = _theme.SecondaryColor;
        }

        private void Name_TextBox_TextChanged(object sender, EventArgs e)
        {
            string text = Name_TextBox.Text;

            string name = string.IsNullOrWhiteSpace(text) ? "Unnamed Theme" : text;

            _theme.Name = name;

            RefreshForm();
        }

        private void OnColorButtonClick(ref Color themeColor)
        {
            if (Theme_ColorDialog.ShowDialog() == DialogResult.OK)
            {
                themeColor = Theme_ColorDialog.Color;

                RefreshForm();
            }
        }

        private void PrimaryColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.PrimaryColor);
        }

        private void SecondaryColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.SecondaryColor);
        }

        private void TertiaryColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.TertiaryColor);
        }

        private void TextColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.TextColor);
        }

        private void KeywordColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.KeywordColor);
        }

        private void RegisterColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.RegisterColor);
        }

        private void NumberColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.NumberColor);
        }

        private void CommentColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.CommentColor);
        }

        private void LabelColor_Button_Click(object sender, EventArgs e)
        {
            OnColorButtonClick(ref _theme.LabelColor);
        }
    }
}
