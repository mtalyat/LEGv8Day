namespace LEGv8Day
{
    partial class OutputForm
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
            this.Output_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.ExecutionTime_Label = new System.Windows.Forms.Label();
            this.OutputTabControl = new System.Windows.Forms.TabControl();
            this.OutputTabPage = new System.Windows.Forms.TabPage();
            this.StackTraceTabPage = new System.Windows.Forms.TabPage();
            this.StackTrace_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.OutputTabControl.SuspendLayout();
            this.OutputTabPage.SuspendLayout();
            this.StackTraceTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Output_RichTextBox
            // 
            this.Output_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Output_RichTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Output_RichTextBox.Location = new System.Drawing.Point(6, 6);
            this.Output_RichTextBox.Name = "Output_RichTextBox";
            this.Output_RichTextBox.ReadOnly = true;
            this.Output_RichTextBox.Size = new System.Drawing.Size(1027, 485);
            this.Output_RichTextBox.TabIndex = 0;
            this.Output_RichTextBox.Text = "";
            // 
            // ExecutionTime_Label
            // 
            this.ExecutionTime_Label.AutoSize = true;
            this.ExecutionTime_Label.Location = new System.Drawing.Point(12, 9);
            this.ExecutionTime_Label.Name = "ExecutionTime_Label";
            this.ExecutionTime_Label.Size = new System.Drawing.Size(125, 15);
            this.ExecutionTime_Label.TabIndex = 1;
            this.ExecutionTime_Label.Text = "Execution Time: 0.0ms";
            // 
            // OutputTabControl
            // 
            this.OutputTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputTabControl.Controls.Add(this.OutputTabPage);
            this.OutputTabControl.Controls.Add(this.StackTraceTabPage);
            this.OutputTabControl.Location = new System.Drawing.Point(12, 27);
            this.OutputTabControl.Name = "OutputTabControl";
            this.OutputTabControl.SelectedIndex = 0;
            this.OutputTabControl.Size = new System.Drawing.Size(1047, 525);
            this.OutputTabControl.TabIndex = 2;
            // 
            // OutputTabPage
            // 
            this.OutputTabPage.Controls.Add(this.Output_RichTextBox);
            this.OutputTabPage.Location = new System.Drawing.Point(4, 24);
            this.OutputTabPage.Name = "OutputTabPage";
            this.OutputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.OutputTabPage.Size = new System.Drawing.Size(1039, 497);
            this.OutputTabPage.TabIndex = 0;
            this.OutputTabPage.Text = "Output";
            this.OutputTabPage.UseVisualStyleBackColor = true;
            // 
            // StackTraceTabPage
            // 
            this.StackTraceTabPage.Controls.Add(this.StackTrace_RichTextBox);
            this.StackTraceTabPage.Location = new System.Drawing.Point(4, 24);
            this.StackTraceTabPage.Name = "StackTraceTabPage";
            this.StackTraceTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.StackTraceTabPage.Size = new System.Drawing.Size(1039, 497);
            this.StackTraceTabPage.TabIndex = 1;
            this.StackTraceTabPage.Text = "Stack Trace";
            this.StackTraceTabPage.UseVisualStyleBackColor = true;
            // 
            // StackTrace_RichTextBox
            // 
            this.StackTrace_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StackTrace_RichTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StackTrace_RichTextBox.Location = new System.Drawing.Point(6, 6);
            this.StackTrace_RichTextBox.Name = "StackTrace_RichTextBox";
            this.StackTrace_RichTextBox.ReadOnly = true;
            this.StackTrace_RichTextBox.Size = new System.Drawing.Size(1027, 485);
            this.StackTrace_RichTextBox.TabIndex = 1;
            this.StackTrace_RichTextBox.Text = "";
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 564);
            this.Controls.Add(this.OutputTabControl);
            this.Controls.Add(this.ExecutionTime_Label);
            this.Name = "OutputForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Emulation";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            this.OutputTabControl.ResumeLayout(false);
            this.OutputTabPage.ResumeLayout(false);
            this.StackTraceTabPage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox Output_RichTextBox;
        private Label ExecutionTime_Label;
        private TabControl OutputTabControl;
        private TabPage OutputTabPage;
        private TabPage StackTraceTabPage;
        private RichTextBox StackTrace_RichTextBox;
    }
}