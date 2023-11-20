using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                MessageBox.Show("You did not select an image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            if (baseImage == null) {
                MessageBox.Show("Base image does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int width = baseImage.Width;
            int height = baseImage.Height;

            processedImage = new Bitmap(width, height);
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Color pixel = baseImage.GetPixel(x, y);
                    processedImage.SetPixel(x, y, pixel);
                }
            }
            pictureBox2.Image = processedImage;
        }

        private void greyscaleToolStripMenuItem1_Click(object sender, EventArgs e) {

        }
    }
}
