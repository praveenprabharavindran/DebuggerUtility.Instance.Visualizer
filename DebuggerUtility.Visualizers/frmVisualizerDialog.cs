/*************************************************************************
*
*   Class/Module Name: frmVisualizerDialog
*
*   Date Created:  01/Aug/2011, Praveen P R
*
*   Purpose: UI for Visualizer classes.
*
*   Date            Author          Description
*   ----            ------          -----------
*   01/Aug/2011    Praveen P R      Created the class file.
*************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;

namespace DebuggerUtility.Visualizers
{
    public partial class frmVisualizerDialog : Form
    {
    #region Fields
        private string restoreString;
        private const int MIN_HEIGHT = 50;
        private const int MIN_WIDTH = 100;
    #endregion Fields

    #region Constants
        
        
        private const string FORM_NAME_BINARY_VISUALIZER = "Binary Visualizer.";
    #endregion Constants

    #region Constructors
        public frmVisualizerDialog(DebuggerSideVisualizer argDebuggerSideVisualizer)
        {
            VisualizerInstance = argDebuggerSideVisualizer;
            InitializeComponent();
        }
    #endregion Constructors

    #region Properties
        /// <summary>
        /// This property holds the DebuggerSizeVisualiser instance the form is dealing with
        /// </summary>
        public DebuggerSideVisualizer VisualizerInstance { get; set; }
    #endregion Properties

    #region Events
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                VisualizerInstance.IsUpdateRequired = true;
                VisualizerInstance.UpdateTargetObject(txtVisualizer.Text);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show("The deserialization of the xml to object failed. Click the restore button to restore to original xml."
                + "Exception details: " + exception.Message);
            }
        }

        private void frmVisualizerDialog_Load(object sender, EventArgs e)
        {
            RePositionControls();

            if(VisualizerInstance.IsEditable == false)
            {
                this.MaximizeBox = false;
                btnUpdate.Visible = false;
                btnRestoreXml.Visible = false;
                txtVisualizer.Visible = false;
            }

            txtVisualizer.Text = restoreString = VisualizerInstance.FormattedString;
            this.Text = VisualizerInstance.Name;
        }

        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            string openFileName = GetOpenFileName();
            if (string.IsNullOrWhiteSpace(openFileName))
            {
                return;
            }

            VisualizerInstance.LoadFromFile(openFileName);
            if (VisualizerInstance.IsEditable == true)
            {
                txtVisualizer.Text = VisualizerInstance.FormattedString;
            }
        }

        private void btnRestoreXml_Click(object sender, EventArgs e)
        {
            txtVisualizer.Text = restoreString;
        }

        private void frmVisualizerDialog_Resize(object sender, EventArgs e)
        {
            RePositionControls();
        }

        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            string fileName = GetSaveFileName();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return;
            }
            VisualizerInstance.UpdateTargetObject(txtVisualizer.Text);
            VisualizerInstance.SaveToFile(fileName);
        }

    #endregion Events

        private void RePositionControls()
        {
            //If the visualizer is editable then hide the edit textbox.
            if (VisualizerInstance.IsEditable == false)
            {
                this.Size = new Size(MIN_WIDTH*3, 75);
                btnLoadFromFile.Location = new Point(txtVisualizer.Location.X + 10, txtVisualizer.Location.Y + 5);
                btnSaveToFile.Location = new Point(btnLoadFromFile.Location.X + btnLoadFromFile.Width + 25, txtVisualizer.Location.Y + 5);
                return;
            }

            int textHeight = txtVisualizer.Size.Height;
            int textWidth = txtVisualizer.Size.Width;

            int updateButtonX = btnUpdate.Location.X;
            int updateButtonY = btnUpdate.Location.Y;

            int restoreButtonX = btnRestoreXml.Location.X;
            int restoreButtonY = btnRestoreXml.Location.Y;

            int saveToFileButtonX = btnSaveToFile.Location.X;
            int saveToFileButtonY = btnSaveToFile.Location.Y;

            int loadFromFileButtonX = btnLoadFromFile.Location.X;
            int loadFromFileButtonY = btnLoadFromFile.Location.Y;

            if (this.Size.Height > MIN_HEIGHT)
            {
                textHeight = this.Size.Height -70;
                updateButtonY = this.Size.Height - 65;
                restoreButtonY = this.Size.Height - 65;
                saveToFileButtonY = this.Size.Height - 65;
                loadFromFileButtonY = this.Size.Height - 65;
            }

            if (this.Size.Width > MIN_WIDTH)
            {
                textWidth = this.Size.Width - 15;
                restoreButtonX = this.Size.Width - btnRestoreXml.Width -50 ;
                updateButtonX = restoreButtonX - btnUpdate.Width - 15;
                saveToFileButtonX = updateButtonX - btnSaveToFile.Width - 15;
                loadFromFileButtonX = saveToFileButtonX - btnLoadFromFile.Width - 15;
            }

            txtVisualizer.Size = new Size(textWidth, textHeight);
            btnUpdate.Location = new Point(updateButtonX, updateButtonY);
            btnRestoreXml.Location = new Point(restoreButtonX, restoreButtonY);
            btnSaveToFile.Location = new Point(saveToFileButtonX, saveToFileButtonY);
            btnLoadFromFile.Location = new Point(loadFromFileButtonX, loadFromFileButtonY);
        }
        
        /// <summary>
        /// Gets the filename for save  from user input.
        /// </summary>
        /// <returns>>Path of the file to save.</returns>
        private string GetSaveFileName()
        {
            string returnFileName = string.Empty;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            if (VisualizerInstance.IsEditable)
            {
                saveFileDialog.Filter = "Xml File|*.Xml";
                saveFileDialog.Title = "Save as an Xml File";
            }
            else
            {
                saveFileDialog.Filter = "Vbin File|*.Vbin";
                saveFileDialog.Title = "Save as an Visualizer Binary File";
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                returnFileName = saveFileDialog.FileName.Trim();
            }
            return returnFileName;
        }

        /// <summary>
        /// Gets the filename to be opened from user input.
        /// </summary>
        /// <returns>Path of the file to open.</returns>
        private string GetOpenFileName()
        {
            string openFileName = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
           
            if(VisualizerInstance.IsEditable)
            {
                openFileDialog.Filter = "Xml files (*.Xml)|*.Xml|All files (*.*)|*.*";
            }
            else
            {
                openFileDialog.Filter = "Vbin File|*.Vbin";
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                openFileName = openFileDialog.FileName.Trim();
            }
            return openFileName;
        }
    }
}
