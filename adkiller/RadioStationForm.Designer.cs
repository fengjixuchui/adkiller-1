namespace AdKiller
{
    partial class RadioStationForm
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
            this.components = new System.ComponentModel.Container();
            this.stationsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.AddRadioStationButton = new System.Windows.Forms.Button();
            this.EditRadioStationButton = new System.Windows.Forms.Button();
            this.DeleteRadioStationButton = new System.Windows.Forms.Button();
            this.radioStationListBox = new System.Windows.Forms.ListBox();
            this.radioPropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.radioConfigurationBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.stationsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioConfigurationBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // stationsBindingSource
            // 
            this.stationsBindingSource.DataMember = "Stations";
            this.stationsBindingSource.DataSource = this.radioConfigurationBindingSource;
            // 
            // AddRadioStationButton
            // 
            this.AddRadioStationButton.Location = new System.Drawing.Point(12, 27);
            this.AddRadioStationButton.Name = "AddRadioStationButton";
            this.AddRadioStationButton.Size = new System.Drawing.Size(55, 23);
            this.AddRadioStationButton.TabIndex = 1;
            this.AddRadioStationButton.Text = "Add ";
            this.AddRadioStationButton.UseVisualStyleBackColor = true;
            // 
            // EditRadioStationButton
            // 
            this.EditRadioStationButton.Location = new System.Drawing.Point(12, 56);
            this.EditRadioStationButton.Name = "EditRadioStationButton";
            this.EditRadioStationButton.Size = new System.Drawing.Size(55, 23);
            this.EditRadioStationButton.TabIndex = 2;
            this.EditRadioStationButton.Text = "Edit";
            this.EditRadioStationButton.UseVisualStyleBackColor = true;
            // 
            // DeleteRadioStationButton
            // 
            this.DeleteRadioStationButton.Location = new System.Drawing.Point(12, 85);
            this.DeleteRadioStationButton.Name = "DeleteRadioStationButton";
            this.DeleteRadioStationButton.Size = new System.Drawing.Size(55, 23);
            this.DeleteRadioStationButton.TabIndex = 3;
            this.DeleteRadioStationButton.Text = "Delete";
            this.DeleteRadioStationButton.UseVisualStyleBackColor = true;
            // 
            // radioStationListBox
            // 
            this.radioStationListBox.FormattingEnabled = true;
            this.radioStationListBox.Location = new System.Drawing.Point(85, 12);
            this.radioStationListBox.Name = "radioStationListBox";
            this.radioStationListBox.Size = new System.Drawing.Size(98, 134);
            this.radioStationListBox.TabIndex = 4;
            // 
            // radioPropertyGrid
            // 
            this.radioPropertyGrid.Location = new System.Drawing.Point(189, 12);
            this.radioPropertyGrid.Name = "radioPropertyGrid";
            this.radioPropertyGrid.Size = new System.Drawing.Size(227, 169);
            this.radioPropertyGrid.TabIndex = 5;
            // 
            // radioConfigurationBindingSource
            // 
            this.radioConfigurationBindingSource.DataSource = typeof(AdKiller.RadioConfiguration);
            // 
            // RadioStationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 199);
            this.Controls.Add(this.radioPropertyGrid);
            this.Controls.Add(this.radioStationListBox);
            this.Controls.Add(this.DeleteRadioStationButton);
            this.Controls.Add(this.EditRadioStationButton);
            this.Controls.Add(this.AddRadioStationButton);
            this.MaximizeBox = false;
            this.Name = "RadioStationForm";
            this.Text = "RadioStatioForm";
            ((System.ComponentModel.ISupportInitialize)(this.stationsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioConfigurationBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource stationsBindingSource;
        private System.Windows.Forms.BindingSource radioConfigurationBindingSource;
        private System.Windows.Forms.Button AddRadioStationButton;
        private System.Windows.Forms.Button EditRadioStationButton;
        private System.Windows.Forms.Button DeleteRadioStationButton;
        private System.Windows.Forms.ListBox radioStationListBox;
        private System.Windows.Forms.PropertyGrid radioPropertyGrid;
    }
}