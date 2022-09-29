namespace LEGv8Day
{
    partial class RunForm
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
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.ElapsedTime_Label = new System.Windows.Forms.Label();
            this.State_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Cancel_Button.Location = new System.Drawing.Point(12, 61);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Button.TabIndex = 0;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = true;
            this.Cancel_Button.Click += new System.EventHandler(this.Cancel_Button_Click);
            // 
            // ElapsedTime_Label
            // 
            this.ElapsedTime_Label.AutoSize = true;
            this.ElapsedTime_Label.Location = new System.Drawing.Point(12, 35);
            this.ElapsedTime_Label.Name = "ElapsedTime_Label";
            this.ElapsedTime_Label.Size = new System.Drawing.Size(107, 15);
            this.ElapsedTime_Label.TabIndex = 1;
            this.ElapsedTime_Label.Text = "Elapsed time: N/As";
            this.ElapsedTime_Label.Visible = false;
            // 
            // State_Label
            // 
            this.State_Label.AutoSize = true;
            this.State_Label.Location = new System.Drawing.Point(12, 9);
            this.State_Label.Name = "State_Label";
            this.State_Label.Size = new System.Drawing.Size(52, 15);
            this.State_Label.TabIndex = 2;
            this.State_Label.Text = "Running";
            // 
            // RunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 96);
            this.ControlBox = false;
            this.Controls.Add(this.State_Label);
            this.Controls.Add(this.ElapsedTime_Label);
            this.Controls.Add(this.Cancel_Button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "RunForm";
            this.Text = "Running Name...";
            this.Load += new System.EventHandler(this.RunForm_Load);
            this.Shown += new System.EventHandler(this.RunForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button Cancel_Button;
        private Label ElapsedTime_Label;
        private Label State_Label;
    }
}