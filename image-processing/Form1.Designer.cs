
namespace image_processing {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.saveImageToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picturebox1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picturebox2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.processedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtractedpb3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greyscaleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.colorInversionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subtractionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cameraPlacementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picturebox1ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.picturebox2ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.cameraFilterLabel = new System.Windows.Forms.Label();
            this.comboBoxCameraFilters = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCamRes = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 65);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(492, 493);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(525, 65);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(492, 493);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // saveImageToolStripMenuItem1
            // 
            this.saveImageToolStripMenuItem1.Name = "saveImageToolStripMenuItem1";
            this.saveImageToolStripMenuItem1.Size = new System.Drawing.Size(270, 34);
            this.saveImageToolStripMenuItem1.Text = "Save Image";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.imageProcessingToolStripMenuItem,
            this.cameraPlacementToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1543, 33);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openImageToolStripMenuItem,
            this.saveImageToolStripMenuItem2});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openImageToolStripMenuItem
            // 
            this.openImageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.picturebox1ToolStripMenuItem,
            this.picturebox2ToolStripMenuItem});
            this.openImageToolStripMenuItem.Name = "openImageToolStripMenuItem";
            this.openImageToolStripMenuItem.Size = new System.Drawing.Size(208, 34);
            this.openImageToolStripMenuItem.Text = "Load Image";
            this.openImageToolStripMenuItem.Click += new System.EventHandler(this.openImageToolStripMenuItem_Click);
            // 
            // picturebox1ToolStripMenuItem
            // 
            this.picturebox1ToolStripMenuItem.Name = "picturebox1ToolStripMenuItem";
            this.picturebox1ToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.picturebox1ToolStripMenuItem.Text = "Picturebox1";
            this.picturebox1ToolStripMenuItem.Click += new System.EventHandler(this.picturebox1ToolStripMenuItem_Click);
            // 
            // picturebox2ToolStripMenuItem
            // 
            this.picturebox2ToolStripMenuItem.Name = "picturebox2ToolStripMenuItem";
            this.picturebox2ToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.picturebox2ToolStripMenuItem.Text = "Picturebox2";
            this.picturebox2ToolStripMenuItem.Click += new System.EventHandler(this.picturebox2ToolStripMenuItem_Click);
            // 
            // saveImageToolStripMenuItem2
            // 
            this.saveImageToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processedToolStripMenuItem,
            this.subtractedpb3ToolStripMenuItem,
            this.pictureBox3ToolStripMenuItem});
            this.saveImageToolStripMenuItem2.Name = "saveImageToolStripMenuItem2";
            this.saveImageToolStripMenuItem2.Size = new System.Drawing.Size(208, 34);
            this.saveImageToolStripMenuItem2.Text = "Save Image";
            this.saveImageToolStripMenuItem2.Click += new System.EventHandler(this.saveImageToolStripMenuItem2_Click);
            // 
            // processedToolStripMenuItem
            // 
            this.processedToolStripMenuItem.Name = "processedToolStripMenuItem";
            this.processedToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.processedToolStripMenuItem.Text = "pictureBox1";
            this.processedToolStripMenuItem.Click += new System.EventHandler(this.processedToolStripMenuItem_Click);
            // 
            // subtractedpb3ToolStripMenuItem
            // 
            this.subtractedpb3ToolStripMenuItem.Name = "subtractedpb3ToolStripMenuItem";
            this.subtractedpb3ToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.subtractedpb3ToolStripMenuItem.Text = "pictureBox2";
            this.subtractedpb3ToolStripMenuItem.Click += new System.EventHandler(this.subtractedpb3ToolStripMenuItem_Click);
            // 
            // pictureBox3ToolStripMenuItem
            // 
            this.pictureBox3ToolStripMenuItem.Name = "pictureBox3ToolStripMenuItem";
            this.pictureBox3ToolStripMenuItem.Size = new System.Drawing.Size(207, 34);
            this.pictureBox3ToolStripMenuItem.Text = "pictureBox3";
            this.pictureBox3ToolStripMenuItem.Click += new System.EventHandler(this.pictureBox3ToolStripMenuItem_Click);
            // 
            // imageProcessingToolStripMenuItem
            // 
            this.imageProcessingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.greyscaleToolStripMenuItem1,
            this.colorInversionToolStripMenuItem,
            this.histogramToolStripMenuItem,
            this.sepiaToolStripMenuItem,
            this.subtractionToolStripMenuItem});
            this.imageProcessingToolStripMenuItem.Name = "imageProcessingToolStripMenuItem";
            this.imageProcessingToolStripMenuItem.Size = new System.Drawing.Size(168, 29);
            this.imageProcessingToolStripMenuItem.Text = "Image Processing";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // greyscaleToolStripMenuItem1
            // 
            this.greyscaleToolStripMenuItem1.Name = "greyscaleToolStripMenuItem1";
            this.greyscaleToolStripMenuItem1.Size = new System.Drawing.Size(270, 34);
            this.greyscaleToolStripMenuItem1.Text = "Greyscale";
            this.greyscaleToolStripMenuItem1.Click += new System.EventHandler(this.greyscaleToolStripMenuItem1_Click);
            // 
            // colorInversionToolStripMenuItem
            // 
            this.colorInversionToolStripMenuItem.Name = "colorInversionToolStripMenuItem";
            this.colorInversionToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.colorInversionToolStripMenuItem.Text = "Color Inversion";
            this.colorInversionToolStripMenuItem.Click += new System.EventHandler(this.colorInversionToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // sepiaToolStripMenuItem
            // 
            this.sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            this.sepiaToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.sepiaToolStripMenuItem.Text = "Sepia";
            this.sepiaToolStripMenuItem.Click += new System.EventHandler(this.sepiaToolStripMenuItem_Click);
            // 
            // subtractionToolStripMenuItem
            // 
            this.subtractionToolStripMenuItem.Name = "subtractionToolStripMenuItem";
            this.subtractionToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.subtractionToolStripMenuItem.Text = "Subtraction";
            this.subtractionToolStripMenuItem.Click += new System.EventHandler(this.subtractionToolStripMenuItem_Click);
            // 
            // cameraPlacementToolStripMenuItem
            // 
            this.cameraPlacementToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.picturebox1ToolStripMenuItem1,
            this.picturebox2ToolStripMenuItem1});
            this.cameraPlacementToolStripMenuItem.Name = "cameraPlacementToolStripMenuItem";
            this.cameraPlacementToolStripMenuItem.Size = new System.Drawing.Size(174, 29);
            this.cameraPlacementToolStripMenuItem.Text = "Camera Placement";
            // 
            // picturebox1ToolStripMenuItem1
            // 
            this.picturebox1ToolStripMenuItem1.Name = "picturebox1ToolStripMenuItem1";
            this.picturebox1ToolStripMenuItem1.Size = new System.Drawing.Size(207, 34);
            this.picturebox1ToolStripMenuItem1.Text = "Picturebox1";
            this.picturebox1ToolStripMenuItem1.Click += new System.EventHandler(this.picturebox1ToolStripMenuItem1_Click);
            // 
            // picturebox2ToolStripMenuItem1
            // 
            this.picturebox2ToolStripMenuItem1.Name = "picturebox2ToolStripMenuItem1";
            this.picturebox2ToolStripMenuItem1.Size = new System.Drawing.Size(207, 34);
            this.picturebox2ToolStripMenuItem1.Text = "Picturebox2";
            this.picturebox2ToolStripMenuItem1.Click += new System.EventHandler(this.picturebox2ToolStripMenuItem1_Click_1);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Location = new System.Drawing.Point(1037, 65);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(492, 493);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(12, 573);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(245, 34);
            this.comboBoxCameras.TabIndex = 6;
            this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameras_SelectedIndexChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(263, 569);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(99, 44);
            this.btnStart.TabIndex = 7;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(368, 569);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(99, 44);
            this.btnStop.TabIndex = 8;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // cameraFilterLabel
            // 
            this.cameraFilterLabel.AutoSize = true;
            this.cameraFilterLabel.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cameraFilterLabel.Location = new System.Drawing.Point(520, 575);
            this.cameraFilterLabel.Name = "cameraFilterLabel";
            this.cameraFilterLabel.Size = new System.Drawing.Size(160, 26);
            this.cameraFilterLabel.TabIndex = 9;
            this.cameraFilterLabel.Text = "Camera Filter";
            // 
            // comboBoxCameraFilters
            // 
            this.comboBoxCameraFilters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameraFilters.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCameraFilters.FormattingEnabled = true;
            this.comboBoxCameraFilters.Location = new System.Drawing.Point(686, 572);
            this.comboBoxCameraFilters.Name = "comboBoxCameraFilters";
            this.comboBoxCameraFilters.Size = new System.Drawing.Size(169, 34);
            this.comboBoxCameraFilters.TabIndex = 10;
            this.comboBoxCameraFilters.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameraFilters_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(874, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 26);
            this.label1.TabIndex = 12;
            this.label1.Text = "Camera Resolution";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBoxCamRes
            // 
            this.comboBoxCamRes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCamRes.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCamRes.FormattingEnabled = true;
            this.comboBoxCamRes.Location = new System.Drawing.Point(1097, 572);
            this.comboBoxCamRes.Name = "comboBoxCamRes";
            this.comboBoxCamRes.Size = new System.Drawing.Size(104, 34);
            this.comboBoxCamRes.TabIndex = 14;
            this.comboBoxCamRes.SelectedIndexChanged += new System.EventHandler(this.comboBoxCamRes_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1543, 625);
            this.Controls.Add(this.comboBoxCamRes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxCameraFilters);
            this.Controls.Add(this.cameraFilterLabel);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem imageProcessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greyscaleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem colorInversionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripMenuItem processedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subtractedpb3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem picturebox1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem picturebox2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subtractionToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.ToolStripMenuItem cameraPlacementToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem picturebox1ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem picturebox2ToolStripMenuItem1;
        private System.Windows.Forms.Label cameraFilterLabel;
        private System.Windows.Forms.ComboBox comboBoxCameraFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCamRes;
        private System.Windows.Forms.ToolStripMenuItem pictureBox3ToolStripMenuItem;
    }
}

