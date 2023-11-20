using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace image_processing {
    public partial class Form1 : Form {

        Bitmap baseImage;
        Bitmap processedImage;

        public Form1() {
            InitializeComponent();
            this.Text = "Image Processing";
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e) {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {
            try {
                baseImage = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = baseImage;
            } catch {
                ImageProcessing.displayError();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                ImageProcessing.displayError();
                return;
            }
            processedImage = ImageProcessing.basicCopy(baseImage);
            pictureBox2.Image = processedImage;
        }

        private void greyscaleToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                ImageProcessing.displayError();
                return;
            }
            processedImage = ImageProcessing.greyScale(baseImage);
            pictureBox2.Image = processedImage;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                ImageProcessing.displayError();
                return;
            }
            processedImage = ImageProcessing.colorInversion(baseImage);
            pictureBox2.Image = processedImage;
        }
    }
}
