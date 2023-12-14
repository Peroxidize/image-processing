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
        private PictureBox selectedPicturebox;
        private int camWidth = 1280;
        private int camHeight = 720;
        private Boolean cameraMode = false;
        private Boolean formLoading = true;

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
            changePictureBox(ref selectedPicturebox, ref pictureBox1);

            comboBoxCameraFilters.Items.Add("None");
            comboBoxCameraFilters.Items.Add("Copy");
            comboBoxCameraFilters.Items.Add("Greyscale");
            comboBoxCameraFilters.Items.Add("Color Inversion");
            comboBoxCameraFilters.Items.Add("Histogram");
            comboBoxCameraFilters.Items.Add("Sepia");
            comboBoxCameraFilters.Items.Add("Subtraction");
            comboBoxCameraFilters.SelectedIndex = 0;

            comboBoxCamRes.Items.Add("320p");
            comboBoxCamRes.Items.Add("480p");
            comboBoxCamRes.Items.Add("720p");
            comboBoxCamRes.Items.Add("1080p");
            comboBoxCamRes.SelectedIndex = 2;

            formLoading = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            thrashOldCamera();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            // Early return if we've selected the current camera
            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void btnStop_Click(object sender, EventArgs e) {
            thrashOldCamera();
            selectedPicturebox.Image = null;
            selectedPicturebox.Invalidate();
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

        private void processedToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(ref processedImage);
        }

        private void subtractedpb3ToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(ref subtractedImage);
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

        private void picturebox1ToolStripMenuItem1_Click(object sender, EventArgs e) {
            thrashOldCamera();
            selectedPicturebox.Image = null;
            selectedPicturebox.Invalidate();

            changePictureBox(ref selectedPicturebox, ref pictureBox1);

            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void picturebox2ToolStripMenuItem1_Click_1(object sender, EventArgs e) {
            thrashOldCamera();

            changePictureBox(ref selectedPicturebox, ref pictureBox2);

            if (_frameSource != null && _frameSource.Camera == comboBoxCameras.SelectedItem)
                return;

            thrashOldCamera();
            startCapturing();
        }

        private void comboBoxCamRes_SelectedIndexChanged(object sender, EventArgs e) {
            //comboBoxCamRes.Items.Add("320p");
            //comboBoxCamRes.Items.Add("480p");
            //comboBoxCamRes.Items.Add("720p");
            //comboBoxCamRes.Items.Add("1080p");
            if (formLoading) {
                return;
            }
            thrashOldCamera();
            switch (comboBoxCamRes.SelectedIndex) {
                case 0:
                    camWidth = 320;
                    camHeight = 240;
                    break;
                case 1:
                    camWidth = 640;
                    camHeight = 480;
                    break;
                case 2:
                    camWidth = 1280;
                    camHeight = 720;
                    break;
                case 3:
                    camWidth = 1920;
                    camHeight = 1080;
                    break;
            }
            startCapturing();
        }

        private void comboBoxCameraFilters_SelectedIndexChanged(object sender, EventArgs e) {
            //comboBoxCameraFilters.Items.Add("None");
            //comboBoxCameraFilters.Items.Add("Copy");
            //comboBoxCameraFilters.Items.Add("Greyscale");
            //comboBoxCameraFilters.Items.Add("Color Inversion");
            //comboBoxCameraFilters.Items.Add("Histogram");
            //comboBoxCameraFilters.Items.Add("Sepia");
            //comboBoxCameraFilters.Items.Add("Subtraction");
            if (formLoading) {
                return;
            }
            switch (comboBoxCamRes.SelectedIndex) {
                case 0:
                    camWidth = 320;
                    camHeight = 240;
                    break;
                case 1:
                    camWidth = 640;
                    camHeight = 480;
                    break;
                case 2:
                    camWidth = 1280;
                    camHeight = 720;
                    break;
                case 3:
                    camWidth = 1920;
                    camHeight = 1080;
                    break;
            }
        }

        private void saveImageToolStripMenuItem2_Click(object sender, EventArgs e) { }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e) { }

        private void button1_Click(object sender, EventArgs e) { }

        private void button2_Click(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e) { }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }

        private void cameraToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void onToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void offToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void timer1_Tick(object sender, EventArgs e) { }

        private void outputToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void label1_Click(object sender, EventArgs e) { }

        private void textBoxCamWidth_KeyPress(object sender, KeyPressEventArgs e) {}

        private void textBoxCamHeight_KeyPress(object sender, KeyPressEventArgs e) {}

        private void textBoxCamWidth_TextChanged(object sender, EventArgs e) {}

        private void textBoxCamHeight_TextChanged(object sender, EventArgs e) { }

        private void comboBoxCameraHeight_SelectedIndexChanged(object sender, EventArgs e) {}

        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e) {}


        //**************************************************************
        //                           HELPER FUNCTIONS
        //**************************************************************

        private void changePictureBox(
            ref PictureBox targetPictureBox, ref PictureBox newPictureBox
            ) {
            targetPictureBox = newPictureBox;
        }

        private void thrashOldCamera() {
            // Trash the old camera
            if (_frameSource != null) {
                _frameSource.NewFrame -= OnImageCaptured;
                _frameSource.Camera.Dispose();
                setFrameSource(null);
                selectedPicturebox.Paint -= new PaintEventHandler(drawLatestImage);
            }
            cameraMode = false;
            selectedPicturebox.Image = null;
            selectedPicturebox.Invalidate();
        }

        private void startCapturing() {
            try {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = camWidth;
                _frameSource.Camera.CaptureHeight = camHeight;
                //_frameSource.Camera.Fps = 60;
                _frameSource.NewFrame += OnImageCaptured;

                selectedPicturebox.Paint += new PaintEventHandler(drawLatestImage);
                _frameSource.StartFrameCapture();
                cameraMode = true;
            } catch (Exception ex) {
                comboBoxCameras.Text = "Select A Camera";
                MessageBox.Show(ex.Message);
            }
        }

        private Camera CurrentCamera {
            get {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }

        private void setFrameSource(CameraFrameSource cameraFrameSource) {
            if (_frameSource == cameraFrameSource)
                return;

            _frameSource = cameraFrameSource;
        }

        public void OnImageCaptured(Touchless.Vision.Contracts.IFrameSource frameSource, Touchless.Vision.Contracts.Frame frame, double fps) {
            _latestFrame = frame.Image;
            selectedPicturebox.Invalidate();
        }
        
        private void drawLatestImage(object sender, PaintEventArgs e) {
            if (_latestFrame != null) {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, selectedPicturebox.Width, selectedPicturebox.Height);
                Console.WriteLine(_latestFrame.Width + " " + _latestFrame.Height);
            }
            Console.WriteLine(camWidth);
            Console.WriteLine(camHeight);
        }

        private int getFilterIndex() {
            switch (baseFileExtension) {
                case "bmp":
                    return 1;
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

        //**************************************************************
        //                          END HELPER FUNCTIONS
        //**************************************************************
    }
}
