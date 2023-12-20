using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Touchless.Vision.Camera;

namespace image_processing {
    public partial class Form1 : Form {

        private CameraFrameSource _frameSource;
        private static Bitmap _latestFrame;
        private PictureBox selectedPicturebox;
        private int camWidth = 1280;
        private int camHeight = 720;
        private Boolean formLoading = true;
        private int camera_filter = 0;
        private SemaphoreSlim threadSemaphore = new SemaphoreSlim(3); // max threads is 3
        private static bool isDrawing = false;
        private Bitmap _toprocess;

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
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null) {
                showError();
                return;
            }
            pictureBox3.Image = ImageProcessing.basicCopy((Bitmap) pictureBox1.Image);
        }

        private void greyscaleToolStripMenuItem1_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null) {
                showError();
                return;
            }
            pictureBox3.Image = ImageProcessing.greyScale((Bitmap) pictureBox1.Image);
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null) {
                showError();
                return;
            }
            pictureBox3.Image = ImageProcessing.colorInversion((Bitmap) pictureBox1.Image);
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null) {
                showError();
                return;
            }
            pictureBox3.Image = ImageProcessing.histogram((Bitmap) pictureBox1.Image);
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null) {
                showError();
                return;
            }
            pictureBox3.Image = ImageProcessing.sepia((Bitmap) pictureBox1.Image);
        }

        private void subtractionToolStripMenuItem_Click(object sender, EventArgs e) {
            if (pictureBox1.Image == null || pictureBox2.Image == null) {
                showError();
                return;
            }
            int baseWidth = pictureBox1.Image.Width;
            int baseHeight = pictureBox1.Image.Height;
            int bgWidth = pictureBox2.Image.Width;
            int bgHeight = pictureBox2.Image.Height;

            if (baseWidth != bgWidth || baseHeight != bgHeight) {
                MessageBox.Show("Does not share the same resolution", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            pictureBox3.Image = ImageProcessing.subtract(
            (Bitmap)pictureBox1.Image, (Bitmap)pictureBox2.Image);
        }

        private void processedToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(pictureBox1);
        }

        private void subtractedpb3ToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(pictureBox2);
        }
        private void pictureBox3ToolStripMenuItem_Click(object sender, EventArgs e) {
            saveImage(pictureBox3);
        }

        private void picturebox1ToolStripMenuItem_Click(object sender, EventArgs e) {
            openBaseImage(pictureBox1);
        }

        private void picturebox2ToolStripMenuItem_Click(object sender, EventArgs e) {
            openBaseImage(pictureBox2);
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
            if (formLoading == true) {
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

            switch (comboBoxCameraFilters.SelectedIndex) {
                case 0:
                    camera_filter = 0;
                    break;
                case 1:
                    camera_filter = 1;
                    break;
                case 2:
                    camera_filter = 2;
                    break;
                case 3:
                    camera_filter = 3;
                    break;
                case 4:
                    camera_filter = 4;
                    break;
                case 5:
                    camera_filter = 5;
                    break;
                case 6:
                    camera_filter = 6;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {}

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
        }

        private void startCapturing() {
            try {
                Camera c = (Camera)comboBoxCameras.SelectedItem;
                setFrameSource(new CameraFrameSource(c));
                _frameSource.Camera.CaptureWidth = camWidth;
                _frameSource.Camera.CaptureHeight = camHeight;
                //_frameSource.Camera.Fps = 30;
                _frameSource.NewFrame += OnImageCaptured;

                selectedPicturebox.Paint += new PaintEventHandler(drawLatestImage);
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
            // System.InvalidOperationException: 'Object is currently in use elsewhere.'
            _toprocess = (Bitmap)_latestFrame.Clone();
            selectedPicturebox.Image = _latestFrame;
            selectedPicturebox.Invalidate();
        }

        private void drawLatestImage(object sender, PaintEventArgs e) {
            if (_latestFrame != null) {
                // Draw the latest image from the active camera
                e.Graphics.DrawImage(_latestFrame, 0, 0, selectedPicturebox.Width, selectedPicturebox.Height);

                if (camera_filter == 6) { // check for resolution first
                    camera_filter = 0;
                    Bitmap baseImg = (Bitmap)pictureBox1.Image.Clone();
                    Bitmap secondImage = (Bitmap)_latestFrame.Clone();
                    if (pictureBox1.Image == null || pictureBox2.Image == null) {
                        MessageBox.Show("Error: " + "requires two image to perform subtraction", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        comboBoxCameraFilters.SelectedIndex = 0;
                        return;
                    }
                    if (baseImg.Width != secondImage.Width
                        || baseImg.Height != secondImage.Height) {
                        comboBoxCameraFilters.SelectedIndex = 0;
                        MessageBox.Show("Error: " + "Resolution does not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    camera_filter = 6;
                }

                // limit the number of threads to 3
                if (threadSemaphore.Wait(0)) {
                    Thread filterThread = new Thread(() =>
                    {
                        try {
                            filterFrames(_toprocess);
                        } finally {
                            threadSemaphore.Release();
                        }
                    });
                    filterThread.Start();
                } else {
                    // Optionally handle the case when the thread limit is reached
                    Console.WriteLine("Thread limit reached. Skipping filterFrames.");
                }
            }
        }

        private void filterFrames(Bitmap _toprocess) {
            Bitmap toprocess = (Bitmap) _toprocess.Clone();

            switch (camera_filter) {
                case 0:
                    return;
                case 1:
                    break;
                case 2:
                    ImageProcessing.pointer_GreyScale(toprocess);
                    break;
                case 3:
                    ImageProcessing.pointer_ColorInversion(toprocess);
                    break;
                case 4:
                    if (isDrawing) {
                        return;
                    }
                    int[] histogram = ImageProcessing.pointer_Histogram(toprocess);
                    using (Graphics g = pictureBox3.CreateGraphics()) {
                        isDrawing = true;
                        drawHistogram(g, pictureBox3.Size, histogram);
                        g.Dispose();
                    }
                    return;
                case 5:
                    ImageProcessing.pointer_Sepia(toprocess);
                    break;
                case 6:
                    Bitmap baseImg = (Bitmap) pictureBox1.Image.Clone();
                    ImageProcessing.pointer_Subtraction(toprocess, baseImg);
                    break;
            }

            // Update the UI with the processed frame
            BeginInvoke((MethodInvoker)delegate
            {
                pictureBox3.Image = toprocess; // frame is now processed
            });
        }

        private void drawHistogram(Graphics g, Size s, int[] data, Color? BarColor = null) {
            if (BarColor == null) BarColor = Color.Gray;
            Brush BarBrush = new SolidBrush((Color)BarColor);

            int gap = 0;

            float BarWidth = Math.Max(((float)s.Width / (float)256) - gap, 1);
            int MaxData = 1;

            // Get Max Height Data
            for (int i = 0; i < 256; i++) if (data[i] > MaxData) MaxData = data[i];

            // Clear the graphics
            g.Clear(Control.DefaultBackColor);

            for (int i = 0; i < 256; i++) {
                float BarHeight = ((float)s.Height * (float)((float)data[i] / (float)MaxData));
                PointF Location = new PointF(i * (BarWidth + gap), s.Height - BarHeight);
                SizeF Size = new SizeF(BarWidth, BarHeight);
                RectangleF Bounds = new RectangleF(Location, Size);
                g.FillRectangle(BarBrush, Bounds);
            }
            isDrawing = false;
        }

        private Camera CurrentCamera {
            get {
                return comboBoxCameras.SelectedItem as Camera;
            }
        }

        private void openBaseImage(PictureBox pictureBox) {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            try {
                if (openFileDialog.ShowDialog() == DialogResult.OK) {
                    pictureBox.Image = new Bitmap(openFileDialog.FileName);
                }
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveImage(PictureBox pictureBox) {
            if (pictureBox.Image == null) {
                showError();
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg;*.jpeg)|*.jpg;*.jpeg";

            try {
                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    pictureBox.Image.Save(saveFileDialog.FileName);
                }
            } catch (Exception e) {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showError() {
            MessageBox.Show("Error: " + "Picturebox is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //**************************************************************
        //                          END HELPER FUNCTIONS
        //**************************************************************
    }
}
