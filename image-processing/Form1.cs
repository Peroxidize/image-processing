using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace image_processing {
    public partial class Form1 : Form {

        Bitmap baseImage;
        Bitmap processedImage;
        Bitmap subtractedImage;
        String baseFileName, baseFileExtension;

        public Form1() {
            InitializeComponent();
            Text = "Image Processing";
        }
        private void Form1_Load(object sender, EventArgs e) {

        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e) {}

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) {}

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

        private void saveImageToolStripMenuItem2_Click(object sender, EventArgs e) {}

        private void processedToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(ref processedImage);
        }

        private void subtractedpb3ToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(ref subtractedImage);
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) {}

        private void button1_Click(object sender, EventArgs e) {}

        private void button2_Click(object sender, EventArgs e) {}

        private void button3_Click(object sender, EventArgs e) {}

        private int getFilterIndex() {
            switch (baseFileExtension) {
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
        private void setBaseNameExtension(OpenFileDialog openFileDialog) {
            string path = openFileDialog.FileName;
            string[] words = path.Split('\\');
            words = words[words.Length - 1].Split('.');
            baseFileExtension = words[words.Length - 1];
            baseFileName = words[words.Length - 2];
        }

        private void openBaseImage(PictureBox pictureBox, ref Bitmap image) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    image = new Bitmap(openFileDialog.FileName);
                    pictureBox.Image = image;
                    if (image != baseImage) {
                        return;
                    }
                    setBaseNameExtension(openFileDialog);
                }
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void picturebox1ToolStripMenuItem_Click(object sender, EventArgs e) {
            openBaseImage(pictureBox1, ref baseImage);
        }

        private void picturebox2ToolStripMenuItem_Click(object sender, EventArgs e) {
            openBaseImage(pictureBox2, ref processedImage);
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null || processedImage == null) {
                ImageProcessing.displayError();
                return;
            }
            int baseWidth = baseImage.Width;
            int baseHeight = baseImage.Height;
            int bgWidth = processedImage.Width;
            int bgHeight = processedImage.Height;

            if (baseWidth != bgWidth || baseHeight != bgHeight) {
                MessageBox.Show("Does not share the same resolution", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            subtractedImage = ImageProcessing.subtract(baseImage, processedImage);
            pictureBox3.Image = subtractedImage;
        }

        private void saveImage(ref Bitmap image) {
            if (image == null) {
                MessageBox.Show("Image does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = baseFileName;
            saveFileDialog.Filter = "BMP Files (*.bmp)|*.bmp|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg|GIF Files (*.gif)|*.gif|PNG Files (*.png)|*.png|TIFF Files (*.tif;*.tiff)|*.tif;*.tiff|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = getFilterIndex();

            try {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    image.Save(saveFileDialog.FileName);
                }
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
