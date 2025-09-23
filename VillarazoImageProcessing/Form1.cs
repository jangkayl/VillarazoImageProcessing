using AForge.Video;
using AForge.Video.DirectShow;

namespace VillarazoImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap imageA, imageB;
        bool isSubtractionMode = false;

        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(713, 406);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private bool isNoImageA()
        {
            if (imageA == null)
            {
                MessageBox.Show(this,
                    "No image loaded. Please load an image first!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return true;
            }

            return false;
        }

        private void useCameraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("No camera found on this device!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

                videoSource.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting camera: " + ex.Message,
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            pictureBox1.Image = frame;

            imageA = (Bitmap)frame.Clone();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
            base.OnFormClosing(e);
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageA = new Bitmap(openFileDialog.FileName);
                pictureBox1.Image = imageA;
            }
        }

        private void btnLoadBackground_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imageB = new Bitmap(openFileDialog.FileName);
                pictureBox2.Image = imageB;
            }
        }

        private void clearImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
                videoSource = null;
            }

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
                pictureBox2.Image = null;
            }
            if (pictureBox3.Image != null)
            {
                pictureBox3.Image.Dispose();
                pictureBox3.Image = null;
            }

            imageA = null;
            imageB = null;

            isSubtractionMode = false;

            this.Size = new Size(713, 406);
            imageSubtractionToolStripMenuItem.Text = "Image Subtraction";

            MessageBox.Show("All images cleared.", "Clear",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;
            imageB = new Bitmap(imageA);
            pictureBox2.Image = imageB;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;
            imageB = new Bitmap(imageA);

            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color originalColor = imageB.GetPixel(x, y);
                    int greyValue = (originalColor.R + originalColor.G + originalColor.B) / 3;
                    imageB.SetPixel(x, y, Color.FromArgb(greyValue, greyValue, greyValue));
                }
            }

            pictureBox2.Image = imageB;
        }

        private void colorInversionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;
            imageB = new Bitmap(imageA);

            for (int x = 0; x < imageB.Width; x++)
            {
                for (int y = 0; y < imageB.Height; y++)
                {
                    Color originalColor = imageB.GetPixel(x, y);
                    imageB.SetPixel(x, y, Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B));
                }
            }

            pictureBox2.Image = imageB;
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;

            int[] hist = new int[256];
            for (int x = 0; x < imageA.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    hist[grey]++;
                }
            }

            int max = 0;
            foreach (int val in hist)
                if (val > max) max = val;

            imageB = new Bitmap(256, 200);
            using (Graphics g = Graphics.FromImage(imageB))
            {
                g.Clear(Color.White);
                for (int i = 0; i < 256; i++)
                {
                    int height = (int)(hist[i] * 200 / (float)max);
                    g.DrawLine(Pens.Black, new Point(i, 200), new Point(i, 200 - height));
                }
            }
            pictureBox2.Image = imageB;
        }

        private void sepiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;
            imageB = new Bitmap(imageA);

            for (int x = 0; x < imageA.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);
                    int tr = (int)(0.393 * pixel.R + 0.769 * pixel.G + 0.189 * pixel.B);
                    int tg = (int)(0.349 * pixel.R + 0.686 * pixel.G + 0.168 * pixel.B);
                    int tb = (int)(0.272 * pixel.R + 0.534 * pixel.G + 0.131 * pixel.B);

                    tr = Math.Min(255, tr);
                    tg = Math.Min(255, tg);
                    tb = Math.Min(255, tb);

                    imageB.SetPixel(x, y, Color.FromArgb(tr, tg, tb));
                }
            }
            pictureBox2.Image = imageB;
        }

        private void imageSubtractionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isSubtractionMode)
            {
                isSubtractionMode = true;
                imageSubtractionToolStripMenuItem.Text = "Back to Features";
                this.Size = new Size(713, 815);
            }
            else
            {
                isSubtractionMode = false;
                imageSubtractionToolStripMenuItem.Text = "Image Subtraction";
                this.Size = new Size(713, 406);
            }
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if (imageA == null || imageB == null)
            {
                MessageBox.Show(this,
                    "Both images must be loaded. Please load both images first!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            Bitmap resizedBackground = new Bitmap(imageB, imageA.Width, imageA.Height);
            Bitmap resultImage = new Bitmap(imageA.Width, imageA.Height);
            for (int x = 0; x < imageA.Width; x++)
            {
                for (int y = 0; y < imageA.Height; y++)
                {
                    Color pixel = imageA.GetPixel(x, y);

                    // Convert RGB to Hue
                    float hue = pixel.GetHue();        // 0–360
                    float sat = pixel.GetSaturation(); // 0–1
                    float bri = pixel.GetBrightness(); // 0–1

                    //  Detect green (light or dark)
                    bool isGreen =
                        hue >= 50 && hue <= 160 &&   // Hue range for green
                        sat > 0.2 &&                 // needs some saturation
                        bri > 0.2;                   // not too dark

                    if (isGreen)
                    {
                        resultImage.SetPixel(x, y, resizedBackground.GetPixel(x, y));
                    }
                    else
                    {
                        resultImage.SetPixel(x, y, pixel);
                    }
                }
            }

            pictureBox3.Image = resultImage;
        }

        private void processedImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("No featured image to save!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog.Title = "Save Featured Image";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image.Save(saveFileDialog.FileName);
            }
        }

        private void finalOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox3.Image == null)
            {
                MessageBox.Show("No subtracted image to save!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
            saveFileDialog.Title = "Save Subtracted Image";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox3.Image.Save(saveFileDialog.FileName);
            }
        }
    }
}
