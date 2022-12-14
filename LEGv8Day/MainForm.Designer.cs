namespace LEGv8Day
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_New_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Open_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Save_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_SaveAs_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuStrip_Refresh_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Undo_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Redo_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Theme_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Run_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MainMenuStrip_RunSettings_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_AutoDump_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Documentation_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip_Repository_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Assembly_RichTextBox = new System.Windows.Forms.RichTextBox();
            this.Stats_Label = new System.Windows.Forms.Label();
            this.MainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.runToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1068, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "MainMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_New_ToolStripMenuItem,
            this.MainMenuStrip_Open_ToolStripMenuItem,
            this.MainMenuStrip_Save_ToolStripMenuItem,
            this.MainMenuStrip_SaveAs_ToolStripMenuItem,
            this.toolStripSeparator2,
            this.MainMenuStrip_Refresh_ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MainMenuStrip_New_ToolStripMenuItem
            // 
            this.MainMenuStrip_New_ToolStripMenuItem.Name = "MainMenuStrip_New_ToolStripMenuItem";
            this.MainMenuStrip_New_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MainMenuStrip_New_ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MainMenuStrip_New_ToolStripMenuItem.Text = "New";
            this.MainMenuStrip_New_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_New_ToolStripMenuItem_Click);
            // 
            // MainMenuStrip_Open_ToolStripMenuItem
            // 
            this.MainMenuStrip_Open_ToolStripMenuItem.Name = "MainMenuStrip_Open_ToolStripMenuItem";
            this.MainMenuStrip_Open_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MainMenuStrip_Open_ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MainMenuStrip_Open_ToolStripMenuItem.Text = "Open";
            this.MainMenuStrip_Open_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Open_ToolStripMenuItem_Click);
            // 
            // MainMenuStrip_Save_ToolStripMenuItem
            // 
            this.MainMenuStrip_Save_ToolStripMenuItem.Name = "MainMenuStrip_Save_ToolStripMenuItem";
            this.MainMenuStrip_Save_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MainMenuStrip_Save_ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MainMenuStrip_Save_ToolStripMenuItem.Text = "Save";
            this.MainMenuStrip_Save_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Save_ToolStripMenuItem_Click);
            // 
            // MainMenuStrip_SaveAs_ToolStripMenuItem
            // 
            this.MainMenuStrip_SaveAs_ToolStripMenuItem.Name = "MainMenuStrip_SaveAs_ToolStripMenuItem";
            this.MainMenuStrip_SaveAs_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MainMenuStrip_SaveAs_ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MainMenuStrip_SaveAs_ToolStripMenuItem.Text = "Save As...";
            this.MainMenuStrip_SaveAs_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_SaveAs_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // MainMenuStrip_Refresh_ToolStripMenuItem
            // 
            this.MainMenuStrip_Refresh_ToolStripMenuItem.Name = "MainMenuStrip_Refresh_ToolStripMenuItem";
            this.MainMenuStrip_Refresh_ToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.MainMenuStrip_Refresh_ToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.MainMenuStrip_Refresh_ToolStripMenuItem.Text = "Refresh";
            this.MainMenuStrip_Refresh_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Refresh_ToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_Undo_ToolStripMenuItem,
            this.MainMenuStrip_Redo_ToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // MainMenuStrip_Undo_ToolStripMenuItem
            // 
            this.MainMenuStrip_Undo_ToolStripMenuItem.Name = "MainMenuStrip_Undo_ToolStripMenuItem";
            this.MainMenuStrip_Undo_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MainMenuStrip_Undo_ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.MainMenuStrip_Undo_ToolStripMenuItem.Text = "Undo";
            this.MainMenuStrip_Undo_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Undo_ToolStripMenuItem_Click);
            // 
            // MainMenuStrip_Redo_ToolStripMenuItem
            // 
            this.MainMenuStrip_Redo_ToolStripMenuItem.Name = "MainMenuStrip_Redo_ToolStripMenuItem";
            this.MainMenuStrip_Redo_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.MainMenuStrip_Redo_ToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.MainMenuStrip_Redo_ToolStripMenuItem.Text = "Redo";
            this.MainMenuStrip_Redo_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Redo_ToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_Theme_ToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // MainMenuStrip_Theme_ToolStripMenuItem
            // 
            this.MainMenuStrip_Theme_ToolStripMenuItem.Name = "MainMenuStrip_Theme_ToolStripMenuItem";
            this.MainMenuStrip_Theme_ToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.MainMenuStrip_Theme_ToolStripMenuItem.Text = "Theme";
            this.MainMenuStrip_Theme_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Theme_ToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            this.runToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_Run_ToolStripMenuItem,
            this.toolStripSeparator3,
            this.MainMenuStrip_RunSettings_ToolStripMenuItem});
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.runToolStripMenuItem.Text = "Run";
            // 
            // MainMenuStrip_Run_ToolStripMenuItem
            // 
            this.MainMenuStrip_Run_ToolStripMenuItem.Name = "MainMenuStrip_Run_ToolStripMenuItem";
            this.MainMenuStrip_Run_ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MainMenuStrip_Run_ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.MainMenuStrip_Run_ToolStripMenuItem.Text = "Run";
            this.MainMenuStrip_Run_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Run_ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(133, 6);
            // 
            // MainMenuStrip_RunSettings_ToolStripMenuItem
            // 
            this.MainMenuStrip_RunSettings_ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_AutoDump_ToolStripMenuItem});
            this.MainMenuStrip_RunSettings_ToolStripMenuItem.Name = "MainMenuStrip_RunSettings_ToolStripMenuItem";
            this.MainMenuStrip_RunSettings_ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.MainMenuStrip_RunSettings_ToolStripMenuItem.Text = "Settings";
            // 
            // MainMenuStrip_AutoDump_ToolStripMenuItem
            // 
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.CheckOnClick = true;
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.Name = "MainMenuStrip_AutoDump_ToolStripMenuItem";
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.Text = "Auto Dump";
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.ToolTipText = "Should the emulation DUMP at the end of the program?";
            this.MainMenuStrip_AutoDump_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_AutoDump_ToolStripMenuItem_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MainMenuStrip_Documentation_ToolStripMenuItem,
            this.MainMenuStrip_Repository_ToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // MainMenuStrip_Documentation_ToolStripMenuItem
            // 
            this.MainMenuStrip_Documentation_ToolStripMenuItem.Name = "MainMenuStrip_Documentation_ToolStripMenuItem";
            this.MainMenuStrip_Documentation_ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.MainMenuStrip_Documentation_ToolStripMenuItem.Text = "Documentation";
            this.MainMenuStrip_Documentation_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Documentation_ToolStripMenuItem_Click);
            // 
            // MainMenuStrip_Repository_ToolStripMenuItem
            // 
            this.MainMenuStrip_Repository_ToolStripMenuItem.Name = "MainMenuStrip_Repository_ToolStripMenuItem";
            this.MainMenuStrip_Repository_ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.MainMenuStrip_Repository_ToolStripMenuItem.Text = "Repository";
            this.MainMenuStrip_Repository_ToolStripMenuItem.Click += new System.EventHandler(this.MainMenuStrip_Repository_ToolStripMenuItem_Click);
            // 
            // Assembly_RichTextBox
            // 
            this.Assembly_RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Assembly_RichTextBox.Location = new System.Drawing.Point(12, 27);
            this.Assembly_RichTextBox.Name = "Assembly_RichTextBox";
            this.Assembly_RichTextBox.Size = new System.Drawing.Size(1044, 504);
            this.Assembly_RichTextBox.TabIndex = 1;
            this.Assembly_RichTextBox.Text = "";
            this.Assembly_RichTextBox.WordWrap = false;
            this.Assembly_RichTextBox.SelectionChanged += new System.EventHandler(this.Assembly_RichTextBox_SelectionChanged);
            this.Assembly_RichTextBox.TextChanged += new System.EventHandler(this.MainRichTextBox_TextChanged);
            // 
            // Stats_Label
            // 
            this.Stats_Label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Stats_Label.AutoSize = true;
            this.Stats_Label.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Stats_Label.Location = new System.Drawing.Point(12, 534);
            this.Stats_Label.Name = "Stats_Label";
            this.Stats_Label.Size = new System.Drawing.Size(84, 14);
            this.Stats_Label.TabIndex = 2;
            this.Stats_Label.Text = "Stats Label";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 558);
            this.Controls.Add(this.Stats_Label);
            this.Controls.Add(this.Assembly_RichTextBox);
            this.Controls.Add(this.MainMenuStrip);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Legv8 Day";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_New_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Open_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Save_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_SaveAs_ToolStripMenuItem;
        private RichTextBox Assembly_RichTextBox;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem runToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Run_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Undo_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Redo_ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem MainMenuStrip_Refresh_ToolStripMenuItem;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Theme_ToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem MainMenuStrip_RunSettings_ToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Documentation_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_Repository_ToolStripMenuItem;
        private ToolStripMenuItem MainMenuStrip_AutoDump_ToolStripMenuItem;
        private Label Stats_Label;
    }
}