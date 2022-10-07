namespace LEGv8Day
{
    partial class SimulationForm
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
            this.SimulationRichTextBox = new System.Windows.Forms.RichTextBox();
            this.ExecutionTime_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SimulationRichTextBox
            // 
            this.SimulationRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SimulationRichTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SimulationRichTextBox.Location = new System.Drawing.Point(12, 27);
            this.SimulationRichTextBox.Name = "SimulationRichTextBox";
            this.SimulationRichTextBox.ReadOnly = true;
            this.SimulationRichTextBox.Size = new System.Drawing.Size(1047, 525);
            this.SimulationRichTextBox.TabIndex = 0;
            this.SimulationRichTextBox.Text = "";
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
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 564);
            this.Controls.Add(this.ExecutionTime_Label);
            this.Controls.Add(this.SimulationRichTextBox);
            this.Name = "SimulationForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulation";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RichTextBox SimulationRichTextBox;
        private Label ExecutionTime_Label;
    }
}