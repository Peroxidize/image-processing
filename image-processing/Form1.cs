using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Touchless.Vision.Camera;

namespace image_processing {
    public partial class Form1 : Form {

        Bitmap baseImage;
        Bitmap processedImage;
        Bitmap subtractedImage;
        String baseFileName, baseFileExtension;

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private Boolean cameraMode = false;

        public Form1() {
            InitializeComponent();
            Text = "Image Processing";
        }
        private void Form1_Load(object sender, EventArgs e) {
            if (!DesignMode) {
                // Refresh the list of available cameras
                comboBoxCameras.Items.Clear();
                foreach (Camera cam in CameraService.AvailableCameras)
                    comboBoxCameras.Items.Add(cam);

                if (comboBoxCameras.Items.Count > 0)
                    comboBoxCameras.SelectedIndex = 0;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            thrashOldCamera();
        }

        private Camera CurrentCamera {
            get {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }

        private void btnStart_Click(object sender, EventArgs e) {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
            cameraMode = true;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            thrashOldCamera();
            cameraMode = false;
        }
        private void thrashOldCamera() {
            // Trash the old camera
            if (_frameSource != null) {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                pictureBox1.Paint -= new PaintEventHandler(drawLatestImage);
            }
        }

        private void startCapturing() {
            try {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                //_frameSource.Camera.CaptureWidth = 1280;
                //_frameSource.Camera.CaptureHeight = 720;
                //_frameSource.Camera.Fps = 30;
                _frameSource.NewFrame += OnImageCaptured;

                pictureBox1.Paint += new PaintEventHandler(drawLatestImage);
                _frameSource.StartFrameCapture();
            } catch (Exception ex) {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource) {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }
        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps) {
            _latestFrame = frame.Image;
            pictureBox1.Invalidate();
        }
        private void drawLatestImage(object sender, PaintEventArgs e) {
            if (_latestFrame != null) {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, _latestFrame.Width, _latestFrame.Height);
            }
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

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e) {}

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

        private void onToolStripMenuItem_Click(object sender, EventArgs e) {
            // on camera
            
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e) {
            // off camera
            
        }

        private void timer1_Tick(object sender, EventArgs e) {

        }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e) { }

    }
}
