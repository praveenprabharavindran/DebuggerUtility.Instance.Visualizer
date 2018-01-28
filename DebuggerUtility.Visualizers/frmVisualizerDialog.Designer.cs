namespace DebuggerUtility.Visualizers
{
    partial class frmVisualizerDialog
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
            this.btnRestoreXml = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtVisualizer = new System.Windows.Forms.TextBox();
            this.btnSaveToFile = new System.Windows.Forms.Button();
            this.btnLoadFromFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRestoreXml
            // 
            this.btnRestoreXml.Location = new System.Drawing.Point(603, 355);
            this.btnRestoreXml.Name = "btnRestoreXml";
            this.btnRestoreXml.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreXml.TabIndex = 5;
            this.btnRestoreXml.Text = "Restore";
            this.btnRestoreXml.UseVisualStyleBackColor = true;
            this.btnRestoreXml.Click += new System.EventHandler(this.btnRestoreXml_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(522, 355);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // txtVisualizer
            // 
            this.txtVisualizer.Location = new System.Drawing.Point(0, 0);
            this.txtVisualizer.Multiline = true;
            this.txtVisualizer.Name = "txtVisualizer";
            this.txtVisualizer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtVisualizer.Size = new System.Drawing.Size(698, 353);
            this.txtVisualizer.TabIndex = 3;
            this.txtVisualizer.WordWrap = false;
            // 
            // btnSaveToFile
            // 
            this.btnSaveToFile.Location = new System.Drawing.Point(374, 354);
            this.btnSaveToFile.Name = "btnSaveToFile";
            this.btnSaveToFile.Size = new System.Drawing.Size(101, 23);
            this.btnSaveToFile.TabIndex = 6;
            this.btnSaveToFile.Text = "Save To File";
            this.btnSaveToFile.UseVisualStyleBackColor = true;
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);
            // 
            // btnLoadFromFile
            // 
            this.btnLoadFromFile.Location = new System.Drawing.Point(250, 354);
            this.btnLoadFromFile.Name = "btnLoadFromFile";
            this.btnLoadFromFile.Size = new System.Drawing.Size(107, 23);
            this.btnLoadFromFile.TabIndex = 7;
            this.btnLoadFromFile.Text = "Load From File";
            this.btnLoadFromFile.UseVisualStyleBackColor = true;
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);
            // 
            // frmVisualizerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 380);
            this.Controls.Add(this.btnLoadFromFile);
            this.Controls.Add(this.btnSaveToFile);
            this.Controls.Add(this.btnRestoreXml);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtVisualizer);
            this.Name = "frmVisualizerDialog";
            this.Text = "DebuggerSideVisualizer";
            this.Load += new System.EventHandler(this.frmVisualizerDialog_Load);
            this.Resize += new System.EventHandler(this.frmVisualizerDialog_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRestoreXml;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.TextBox txtVisualizer;
        private System.Windows.Forms.Button btnSaveToFile;
        private System.Windows.Forms.Button btnLoadFromFile;

    }
}