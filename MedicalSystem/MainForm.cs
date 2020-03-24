using NeuralNetworkLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedicalSystem
{
    public partial class MainForm : Form
    {
        

        public MainForm()
        {
            InitializeComponent();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            if (aboutBox.ShowDialog() == DialogResult.OK)
            {
                aboutBox.Close();
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png)|*.jpg;*.jpeg;*.jpe;*.jfif;*.png";

            if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                return;

            var imagePath = openFileDialog.FileName;

            PictureConverter pictureConverter = new PictureConverter();
            var inputs = pictureConverter.Convert(imagePath).ToArray();
            var result = Program.SystemController.DataNetwork.Predict(inputs).Output;
        }

        private void enterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var enterDataForm = new EnterData();
            var result = enterDataForm.ShowForm();
        }
    }
}
