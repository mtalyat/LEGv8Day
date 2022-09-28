namespace LEGv8Day
{
    partial class ThemeForm
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
            this.Themes_ListBox = new System.Windows.Forms.ListBox();
            this.Add_Button = new System.Windows.Forms.Button();
            this.Remove_Button = new System.Windows.Forms.Button();
            this.Activate_Button = new System.Windows.Forms.Button();
            this.Edit_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Themes_ListBox
            // 
            this.Themes_ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Themes_ListBox.FormattingEnabled = true;
            this.Themes_ListBox.ItemHeight = 15;
            this.Themes_ListBox.Location = new System.Drawing.Point(12, 12);
            this.Themes_ListBox.Name = "Themes_ListBox";
            this.Themes_ListBox.Size = new System.Drawing.Size(120, 394);
            this.Themes_ListBox.TabIndex = 0;
            this.Themes_ListBox.SelectedIndexChanged += new System.EventHandler(this.Themes_ListBox_SelectedIndexChanged);
            // 
            // Add_Button
            // 
            this.Add_Button.Location = new System.Drawing.Point(12, 415);
            this.Add_Button.Name = "Add_Button";
            this.Add_Button.Size = new System.Drawing.Size(52, 23);
            this.Add_Button.TabIndex = 1;
            this.Add_Button.Text = "Add";
            this.Add_Button.UseVisualStyleBackColor = true;
            this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
            // 
            // Remove_Button
            // 
            this.Remove_Button.Location = new System.Drawing.Point(70, 415);
            this.Remove_Button.Name = "Remove_Button";
            this.Remove_Button.Size = new System.Drawing.Size(62, 23);
            this.Remove_Button.TabIndex = 2;
            this.Remove_Button.Text = "Remove";
            this.Remove_Button.UseVisualStyleBackColor = true;
            this.Remove_Button.Click += new System.EventHandler(this.Remove_Button_Click);
            // 
            // Activate_Button
            // 
            this.Activate_Button.Location = new System.Drawing.Point(138, 12);
            this.Activate_Button.Name = "Activate_Button";
            this.Activate_Button.Size = new System.Drawing.Size(116, 23);
            this.Activate_Button.TabIndex = 3;
            this.Activate_Button.Text = "Activate";
            this.Activate_Button.UseVisualStyleBackColor = true;
            this.Activate_Button.Click += new System.EventHandler(this.Activate_Button_Click);
            // 
            // Edit_Button
            // 
            this.Edit_Button.Location = new System.Drawing.Point(138, 41);
            this.Edit_Button.Name = "Edit_Button";
            this.Edit_Button.Size = new System.Drawing.Size(116, 23);
            this.Edit_Button.TabIndex = 4;
            this.Edit_Button.Text = "Edit";
            this.Edit_Button.UseVisualStyleBackColor = true;
            this.Edit_Button.Click += new System.EventHandler(this.Edit_Button_Click);
            // 
            // ThemeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 450);
            this.Controls.Add(this.Edit_Button);
            this.Controls.Add(this.Activate_Button);
            this.Controls.Add(this.Remove_Button);
            this.Controls.Add(this.Add_Button);
            this.Controls.Add(this.Themes_ListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Themes";
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox Themes_ListBox;
        private Button Add_Button;
        private Button Remove_Button;
        private Button Activate_Button;
        private Button Edit_Button;
    }
}