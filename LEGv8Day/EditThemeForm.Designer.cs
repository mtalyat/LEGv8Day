namespace LEGv8Day
{
    partial class EditThemeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TertiaryColor_Button = new System.Windows.Forms.Button();
            this.SecondaryColor_Button = new System.Windows.Forms.Button();
            this.PrimaryColor_Button = new System.Windows.Forms.Button();
            this.Name_TextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Theme_ColorDialog = new System.Windows.Forms.ColorDialog();
            this.Theme_TabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.LabelColor_Button = new System.Windows.Forms.Button();
            this.CommentColor_Button = new System.Windows.Forms.Button();
            this.NumberColor_Button = new System.Windows.Forms.Button();
            this.RegisterColor_Button = new System.Windows.Forms.Button();
            this.KeywordColor_Button = new System.Windows.Forms.Button();
            this.TextColor_Button = new System.Windows.Forms.Button();
            this.Theme_TabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TertiaryColor_Button
            // 
            this.TertiaryColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TertiaryColor_Button.Location = new System.Drawing.Point(6, 64);
            this.TertiaryColor_Button.Name = "TertiaryColor_Button";
            this.TertiaryColor_Button.Size = new System.Drawing.Size(353, 23);
            this.TertiaryColor_Button.TabIndex = 15;
            this.TertiaryColor_Button.Text = "Tertiary Color";
            this.TertiaryColor_Button.UseVisualStyleBackColor = true;
            this.TertiaryColor_Button.Click += new System.EventHandler(this.TertiaryColor_Button_Click);
            // 
            // SecondaryColor_Button
            // 
            this.SecondaryColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SecondaryColor_Button.Location = new System.Drawing.Point(6, 35);
            this.SecondaryColor_Button.Name = "SecondaryColor_Button";
            this.SecondaryColor_Button.Size = new System.Drawing.Size(353, 23);
            this.SecondaryColor_Button.TabIndex = 14;
            this.SecondaryColor_Button.Text = "Secondary Color";
            this.SecondaryColor_Button.UseVisualStyleBackColor = true;
            this.SecondaryColor_Button.Click += new System.EventHandler(this.SecondaryColor_Button_Click);
            // 
            // PrimaryColor_Button
            // 
            this.PrimaryColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PrimaryColor_Button.Location = new System.Drawing.Point(6, 6);
            this.PrimaryColor_Button.Name = "PrimaryColor_Button";
            this.PrimaryColor_Button.Size = new System.Drawing.Size(353, 23);
            this.PrimaryColor_Button.TabIndex = 13;
            this.PrimaryColor_Button.Text = "Primary Color";
            this.PrimaryColor_Button.UseVisualStyleBackColor = true;
            this.PrimaryColor_Button.Click += new System.EventHandler(this.PrimaryColor_Button_Click);
            // 
            // Name_TextBox
            // 
            this.Name_TextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Name_TextBox.Location = new System.Drawing.Point(60, 12);
            this.Name_TextBox.Name = "Name_TextBox";
            this.Name_TextBox.Size = new System.Drawing.Size(325, 23);
            this.Name_TextBox.TabIndex = 12;
            this.Name_TextBox.TextChanged += new System.EventHandler(this.Name_TextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Name:";
            // 
            // Theme_TabControl
            // 
            this.Theme_TabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Theme_TabControl.Controls.Add(this.tabPage1);
            this.Theme_TabControl.Controls.Add(this.tabPage2);
            this.Theme_TabControl.Location = new System.Drawing.Point(12, 41);
            this.Theme_TabControl.Name = "Theme_TabControl";
            this.Theme_TabControl.SelectedIndex = 0;
            this.Theme_TabControl.Size = new System.Drawing.Size(373, 209);
            this.Theme_TabControl.TabIndex = 16;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.PrimaryColor_Button);
            this.tabPage1.Controls.Add(this.TertiaryColor_Button);
            this.tabPage1.Controls.Add(this.SecondaryColor_Button);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(365, 181);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.LabelColor_Button);
            this.tabPage2.Controls.Add(this.CommentColor_Button);
            this.tabPage2.Controls.Add(this.NumberColor_Button);
            this.tabPage2.Controls.Add(this.RegisterColor_Button);
            this.tabPage2.Controls.Add(this.KeywordColor_Button);
            this.tabPage2.Controls.Add(this.TextColor_Button);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(365, 181);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // LabelColor_Button
            // 
            this.LabelColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelColor_Button.Location = new System.Drawing.Point(6, 151);
            this.LabelColor_Button.Name = "LabelColor_Button";
            this.LabelColor_Button.Size = new System.Drawing.Size(353, 23);
            this.LabelColor_Button.TabIndex = 19;
            this.LabelColor_Button.Text = "Label Color";
            this.LabelColor_Button.UseVisualStyleBackColor = true;
            this.LabelColor_Button.Click += new System.EventHandler(this.LabelColor_Button_Click);
            // 
            // CommentColor_Button
            // 
            this.CommentColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CommentColor_Button.Location = new System.Drawing.Point(6, 122);
            this.CommentColor_Button.Name = "CommentColor_Button";
            this.CommentColor_Button.Size = new System.Drawing.Size(353, 23);
            this.CommentColor_Button.TabIndex = 18;
            this.CommentColor_Button.Text = "Comment Color";
            this.CommentColor_Button.UseVisualStyleBackColor = true;
            this.CommentColor_Button.Click += new System.EventHandler(this.CommentColor_Button_Click);
            // 
            // NumberColor_Button
            // 
            this.NumberColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NumberColor_Button.Location = new System.Drawing.Point(6, 93);
            this.NumberColor_Button.Name = "NumberColor_Button";
            this.NumberColor_Button.Size = new System.Drawing.Size(353, 23);
            this.NumberColor_Button.TabIndex = 17;
            this.NumberColor_Button.Text = "Number Color";
            this.NumberColor_Button.UseVisualStyleBackColor = true;
            this.NumberColor_Button.Click += new System.EventHandler(this.NumberColor_Button_Click);
            // 
            // RegisterColor_Button
            // 
            this.RegisterColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RegisterColor_Button.Location = new System.Drawing.Point(6, 64);
            this.RegisterColor_Button.Name = "RegisterColor_Button";
            this.RegisterColor_Button.Size = new System.Drawing.Size(353, 23);
            this.RegisterColor_Button.TabIndex = 16;
            this.RegisterColor_Button.Text = "Register Color";
            this.RegisterColor_Button.UseVisualStyleBackColor = true;
            this.RegisterColor_Button.Click += new System.EventHandler(this.RegisterColor_Button_Click);
            // 
            // KeywordColor_Button
            // 
            this.KeywordColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.KeywordColor_Button.Location = new System.Drawing.Point(6, 35);
            this.KeywordColor_Button.Name = "KeywordColor_Button";
            this.KeywordColor_Button.Size = new System.Drawing.Size(353, 23);
            this.KeywordColor_Button.TabIndex = 15;
            this.KeywordColor_Button.Text = "Keyword Color";
            this.KeywordColor_Button.UseVisualStyleBackColor = true;
            this.KeywordColor_Button.Click += new System.EventHandler(this.KeywordColor_Button_Click);
            // 
            // TextColor_Button
            // 
            this.TextColor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TextColor_Button.Location = new System.Drawing.Point(6, 6);
            this.TextColor_Button.Name = "TextColor_Button";
            this.TextColor_Button.Size = new System.Drawing.Size(353, 23);
            this.TextColor_Button.TabIndex = 14;
            this.TextColor_Button.Text = "Text Color";
            this.TextColor_Button.UseVisualStyleBackColor = true;
            this.TextColor_Button.Click += new System.EventHandler(this.TextColor_Button_Click);
            // 
            // EditThemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 262);
            this.Controls.Add(this.Theme_TabControl);
            this.Controls.Add(this.Name_TextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditThemeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Edit Theme - Name";
            this.TopMost = true;
            this.Theme_TabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button TertiaryColor_Button;
        private Button SecondaryColor_Button;
        private Button PrimaryColor_Button;
        private TextBox Name_TextBox;
        private Label label1;
        private ColorDialog Theme_ColorDialog;
        private TabControl Theme_TabControl;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button LabelColor_Button;
        private Button CommentColor_Button;
        private Button NumberColor_Button;
        private Button RegisterColor_Button;
        private Button KeywordColor_Button;
        private Button TextColor_Button;
    }
}