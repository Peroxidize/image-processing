using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace image_processing {
    public partial class Form1 : Form {

        Bitmap baseImage;
        Bitmap processedImage;
        String baseFileName, baseFileExtension;

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
                string path = openFileDialog1.FileName;
                string[] words = path.Split('\\');
                words = words[words.Length - 1].Split('.');
                baseFileExtension = words[words.Length - 1];
                baseFileName = words[words.Length - 2];
                Console.WriteLine(baseFileName);
                Console.WriteLine(baseFileExtension);
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

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                ImageProcessing.displayError();
                return;
            }
            processedImage = ImageProcessing.histogram(baseImage);
            pictureBox2.Image = processedImage;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                ImageProcessing.displayError();
                return;
            }
            processedImage = ImageProcessing.sepia(baseImage);
            pictureBox2.Image = processedImage;
        }

        private void saveImageToolStripMenuItem2_Click(object sender, EventArgs e) {
            if (processedImage == null) {
                MessageBox.Show("Processed image does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveFileDialog1.FileName = baseFileName;
            saveFileDialog1.Filter = "BMP Files (*.bmp)|*.bmp|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|TIFF Files (*.tif;*.tiff)|*.tif;*.tiff|All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = getFilterIndex();
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {
            processedImage.Save(saveFileDialog1.FileName);
        }

        private int getFilterIndex() {
            switch(baseFileExtension) {
                case "bmp":
                    return 1;
                    break;
                case "jpg":
                    return 2;
                case "jpeg":
                    return 2;
                case "gif":
                    return 3;
                case "png":
                    return 4;
                case "tif":
                    return 5;
                case "tiff":
                    return 5;
                default:
                    return 6;
            }
        }
    }
}
