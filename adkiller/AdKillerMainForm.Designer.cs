namespace AdKiller
{
    partial class AdKillerMainForm
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
            this.listBoxRadio = new System.Windows.Forms.ListBox();
            this.labelRadioList = new System.Windows.Forms.Label();
            this.PlayButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.RadioStationMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripRadioStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxRadio
            // 
            this.listBoxRadio.FormattingEnabled = true;
            this.listBoxRadio.Location = new System.Drawing.Point(12, 46);
            this.listBoxRadio.Name = "listBoxRadio";
            this.listBoxRadio.Size = new System.Drawing.Size(101, 108);
            this.listBoxRadio.TabIndex = 0;
            // 
            // labelRadioList
            // 
            this.labelRadioList.AutoSize = true;
            this.labelRadioList.Location = new System.Drawing.Point(0, 30);
            this.labelRadioList.Name = "labelRadioList";
            this.labelRadioList.Size = new System.Drawing.Size(122, 13);
            this.labelRadioList.TabIndex = 1;
            this.labelRadioList.Text = "Radio stations available:";
            // 
            // PlayButton
            // 
            this.PlayButton.Location = new System.Drawing.Point(184, 81);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(49, 23);
            this.PlayButton.TabIndex = 5;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(132, 81);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(46, 23);
            this.StopButton.TabIndex = 10;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.RestoreDirectory = true;
            this.openFileDialog1.Tag = "";
            this.openFileDialog1.Title = "\"Open Mp3 / Wave file / Ogg file";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RadioStationMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(275, 24);
            this.menuStrip.TabIndex = 11;
            this.menuStrip.Text = "menuStrip1";
            // 
            // RadioStationMenuItem
            // 
            this.RadioStationMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.RadioStationMenuItem.Name = "RadioStationMenuItem";
            this.RadioStationMenuItem.Size = new System.Drawing.Size(88, 20);
            this.RadioStationMenuItem.Text = "Radio station";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // FileTextBox
            // 
            this.FileTextBox.Location = new System.Drawing.Point(132, 55);
            this.FileTextBox.Name = "FileTextBox";
            this.FileTextBox.Size = new System.Drawing.Size(100, 20);
            this.FileTextBox.TabIndex = 12;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRadioStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 194);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(275, 22);
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripRadioStatusLabel
            // 
            this.toolStripRadioStatusLabel.Name = "toolStripRadioStatusLabel";
            this.toolStripRadioStatusLabel.Size = new System.Drawing.Size(38, 17);
            this.toolStripRadioStatusLabel.Text = "Hello!";
            // 
            // AdKillerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(275, 216);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.FileTextBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.labelRadioList);
            this.Controls.Add(this.listBoxRadio);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "AdKillerMainForm";
            this.Text = "AdKiller";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelRadioList;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem RadioStationMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.TextBox FileTextBox;
        public System.Windows.Forms.ListBox listBoxRadio;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripRadioStatusLabel;
    }
}

