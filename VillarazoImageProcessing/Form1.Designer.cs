namespace VillarazoImageProcessing
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            loadImageToolStripMenuItem = new ToolStripMenuItem();
            loadImageToolStripMenuItem1 = new ToolStripMenuItem();
            loadBackgroundToolStripMenuItem = new ToolStripMenuItem();
            useCameraToolStripMenuItem = new ToolStripMenuItem();
            saveImageToolStripMenuItem = new ToolStripMenuItem();
            processedImageToolStripMenuItem = new ToolStripMenuItem();
            finalOutputToolStripMenuItem = new ToolStripMenuItem();
            clearImageToolStripMenuItem = new ToolStripMenuItem();
            featuresToolStripMenuItem = new ToolStripMenuItem();
            basicCopyToolStripMenuItem = new ToolStripMenuItem();
            greyscaleToolStripMenuItem = new ToolStripMenuItem();
            colorInversionToolStripMenuItem = new ToolStripMenuItem();
            histogramToolStripMenuItem = new ToolStripMenuItem();
            sepiaToolStripMenuItem = new ToolStripMenuItem();
            imageSubtractionToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1 = new MenuStrip();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(25, 41);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(315, 315);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Location = new Point(358, 41);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(315, 315);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.Location = new Point(191, 443);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(315, 315);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // loadImageToolStripMenuItem
            // 
            loadImageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadImageToolStripMenuItem1, loadBackgroundToolStripMenuItem, useCameraToolStripMenuItem, saveImageToolStripMenuItem, clearImageToolStripMenuItem });
            loadImageToolStripMenuItem.Name = "loadImageToolStripMenuItem";
            loadImageToolStripMenuItem.Size = new Size(61, 20);
            loadImageToolStripMenuItem.Text = "Options";
            // 
            // loadImageToolStripMenuItem1
            // 
            loadImageToolStripMenuItem1.Name = "loadImageToolStripMenuItem1";
            loadImageToolStripMenuItem1.Size = new Size(180, 22);
            loadImageToolStripMenuItem1.Text = "Load Image";
            loadImageToolStripMenuItem1.Click += loadImageToolStripMenuItem_Click;
            // 
            // loadBackgroundToolStripMenuItem
            // 
            loadBackgroundToolStripMenuItem.Name = "loadBackgroundToolStripMenuItem";
            loadBackgroundToolStripMenuItem.Size = new Size(180, 22);
            loadBackgroundToolStripMenuItem.Text = "Load Background";
            loadBackgroundToolStripMenuItem.Click += btnLoadBackground_Click;
            // 
            // useCameraToolStripMenuItem
            // 
            useCameraToolStripMenuItem.Name = "useCameraToolStripMenuItem";
            useCameraToolStripMenuItem.Size = new Size(180, 22);
            useCameraToolStripMenuItem.Text = "Use Camera";
            useCameraToolStripMenuItem.Click += useCameraToolStripMenuItem_Click;
            // 
            // saveImageToolStripMenuItem
            // 
            saveImageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { processedImageToolStripMenuItem, finalOutputToolStripMenuItem });
            saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            saveImageToolStripMenuItem.Size = new Size(180, 22);
            saveImageToolStripMenuItem.Text = "Save Image";
            // 
            // processedImageToolStripMenuItem
            // 
            processedImageToolStripMenuItem.Name = "processedImageToolStripMenuItem";
            processedImageToolStripMenuItem.Size = new Size(180, 22);
            processedImageToolStripMenuItem.Text = "Processed Image";
            processedImageToolStripMenuItem.Click += processedImageToolStripMenuItem_Click;
            // 
            // finalOutputToolStripMenuItem
            // 
            finalOutputToolStripMenuItem.Name = "finalOutputToolStripMenuItem";
            finalOutputToolStripMenuItem.Size = new Size(180, 22);
            finalOutputToolStripMenuItem.Text = "Final Output";
            finalOutputToolStripMenuItem.Click += finalOutputToolStripMenuItem_Click;
            // 
            // clearImageToolStripMenuItem
            // 
            clearImageToolStripMenuItem.Name = "clearImageToolStripMenuItem";
            clearImageToolStripMenuItem.Size = new Size(180, 22);
            clearImageToolStripMenuItem.Text = "Clear Image";
            clearImageToolStripMenuItem.Click += clearImageToolStripMenuItem_Click;
            // 
            // featuresToolStripMenuItem
            // 
            featuresToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { basicCopyToolStripMenuItem, greyscaleToolStripMenuItem, colorInversionToolStripMenuItem, histogramToolStripMenuItem, sepiaToolStripMenuItem });
            featuresToolStripMenuItem.Name = "featuresToolStripMenuItem";
            featuresToolStripMenuItem.Size = new Size(63, 20);
            featuresToolStripMenuItem.Text = "Features";
            // 
            // basicCopyToolStripMenuItem
            // 
            basicCopyToolStripMenuItem.Name = "basicCopyToolStripMenuItem";
            basicCopyToolStripMenuItem.Size = new Size(160, 22);
            basicCopyToolStripMenuItem.Text = "Basic Copy";
            basicCopyToolStripMenuItem.Click += basicCopyToolStripMenuItem_Click;
            // 
            // greyscaleToolStripMenuItem
            // 
            greyscaleToolStripMenuItem.Name = "greyscaleToolStripMenuItem";
            greyscaleToolStripMenuItem.Size = new Size(160, 22);
            greyscaleToolStripMenuItem.Text = "Greyscale ";
            greyscaleToolStripMenuItem.Click += greyscaleToolStripMenuItem_Click;
            // 
            // colorInversionToolStripMenuItem
            // 
            colorInversionToolStripMenuItem.Name = "colorInversionToolStripMenuItem";
            colorInversionToolStripMenuItem.Size = new Size(160, 22);
            colorInversionToolStripMenuItem.Text = "Color Inversion  ";
            colorInversionToolStripMenuItem.Click += colorInversionToolStripMenuItem_Click;
            // 
            // histogramToolStripMenuItem
            // 
            histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            histogramToolStripMenuItem.Size = new Size(160, 22);
            histogramToolStripMenuItem.Text = "Histogram ";
            histogramToolStripMenuItem.Click += histogramToolStripMenuItem_Click;
            // 
            // sepiaToolStripMenuItem
            // 
            sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            sepiaToolStripMenuItem.Size = new Size(160, 22);
            sepiaToolStripMenuItem.Text = "Sepia  ";
            sepiaToolStripMenuItem.Click += sepiaToolStripMenuItem_Click;
            // 
            // imageSubtractionToolStripMenuItem
            // 
            imageSubtractionToolStripMenuItem.Name = "imageSubtractionToolStripMenuItem";
            imageSubtractionToolStripMenuItem.Size = new Size(116, 20);
            imageSubtractionToolStripMenuItem.Text = "Image Subtraction";
            imageSubtractionToolStripMenuItem.Click += imageSubtractionToolStripMenuItem_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { loadImageToolStripMenuItem, featuresToolStripMenuItem, imageSubtractionToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(5, 2, 0, 2);
            menuStrip1.Size = new Size(697, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // button1
            // 
            button1.Location = new Point(270, 371);
            button1.Name = "button1";
            button1.Size = new Size(155, 55);
            button1.TabIndex = 6;
            button1.Text = "Subtract";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnSubtract_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(697, 768);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            Controls.Add(pictureBox3);
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Villarazo Image Processing";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private ToolStripMenuItem loadImageToolStripMenuItem;
        private ToolStripMenuItem loadImageToolStripMenuItem1;
        private ToolStripMenuItem clearImageToolStripMenuItem;
        private ToolStripMenuItem featuresToolStripMenuItem;
        private ToolStripMenuItem basicCopyToolStripMenuItem;
        private ToolStripMenuItem greyscaleToolStripMenuItem;
        private ToolStripMenuItem colorInversionToolStripMenuItem;
        private ToolStripMenuItem histogramToolStripMenuItem;
        private ToolStripMenuItem sepiaToolStripMenuItem;
        private ToolStripMenuItem imageSubtractionToolStripMenuItem;
        private MenuStrip menuStrip1;
        private Button button1;
        private ToolStripMenuItem loadBackgroundToolStripMenuItem;
        private ToolStripMenuItem useCameraToolStripMenuItem;
        private ToolStripMenuItem saveImageToolStripMenuItem;
        private ToolStripMenuItem processedImageToolStripMenuItem;
        private ToolStripMenuItem finalOutputToolStripMenuItem;
    }
}
