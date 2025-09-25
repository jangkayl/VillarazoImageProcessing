using System.Diagnostics;
using WinFormsTimer = System.Windows.Forms.Timer;
using WebCamLib;

namespace VillarazoImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap imageA, imageB;
        bool isSubtractionMode = false;
        private bool isLiveSubtraction = false;

        private Device[] devices;            
        private Device currentDevice;      
        private Stopwatch fpsTimer = new Stopwatch();
        private int targetFPS = 1;
        private int frameInterval => 1000 / targetFPS;

        private readonly object imageALock = new object(); 
        private WinFormsTimer frameGrabber;
        
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(713, 406);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            pictureBox3.Hide();

            fpsTimer.Start();
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
                devices = DeviceManager.GetAllDevices();
                if (devices == null || devices.Length == 0)
                {
                    MessageBox.Show("No camera found on this device!", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                currentDevice = devices[0];
                currentDevice.ShowWindow(pictureBox1); 

                frameGrabber = new WinFormsTimer();
                frameGrabber.Interval = frameInterval;
                frameGrabber.Tick += FrameGrabber_Tick;
                frameGrabber.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting camera: " + ex.Message,
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrameGrabber_Tick(object sender, EventArgs e)
        {
            try
            {
                currentDevice.Sendmessage();
                if (Clipboard.ContainsImage())
                {
                    using (Bitmap frame = new Bitmap(Clipboard.GetImage()))
                    {
                        if (frame == null) return;

                        Bitmap frameForDisplay = (Bitmap)frame.Clone();
                        Bitmap outputFrame = null;

                        if (isLiveSubtraction && imageB != null)
                        {
                            Bitmap resizedBackground = new Bitmap(imageB, frame.Width, frame.Height);
                            outputFrame = new Bitmap(frame.Width, frame.Height);

                            for (int x = 0; x < frame.Width; x++)
                            {
                                for (int y = 0; y < frame.Height; y++)
                                {
                                    Color pixel = frame.GetPixel(x, y);
                                    float hue = pixel.GetHue();
                                    float sat = pixel.GetSaturation();
                                    float bri = pixel.GetBrightness();

                                    bool isGreen = (hue >= 40 && hue <= 180 && sat > 0.15 && bri > 0.15);
                                    outputFrame.SetPixel(x, y, isGreen ? resizedBackground.GetPixel(x, y) : pixel);
                                }
                            }
                            resizedBackground.Dispose();
                        }

                        if (pictureBox1.IsHandleCreated)
                        {
                            pictureBox1.Invoke((MethodInvoker)delegate
                            {
                                pictureBox1.Image?.Dispose();
                                pictureBox1.Image = (Bitmap)frameForDisplay.Clone();
                            });
                        }

                        if (pictureBox3 != null && pictureBox3.IsHandleCreated && pictureBox3.Visible && outputFrame != null)
                        {
                            pictureBox3.Invoke((MethodInvoker)delegate
                            {
                                pictureBox3.Image?.Dispose();
                                pictureBox3.Image = outputFrame;
                            });
                        }

                        lock (imageALock)
                        {
                            imageA?.Dispose();
                            imageA = (Bitmap)frame.Clone();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Frame grab error: " + ex.Message);
            }
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            await StopCamera();
            base.OnFormClosing(e);
        }

        private async Task StopCamera()
        {
            try
            {
                if (frameGrabber != null)
                {
                    frameGrabber.Stop();
                    frameGrabber.Dispose();
                    frameGrabber = null;
                }

                if (currentDevice != null)
                {
                    currentDevice.Stop();
                    currentDevice = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error stopping camera: " + ex.Message,
                    "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                await StopCamera();

                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (imageA != null)
                    {
                        imageA.Dispose();
                        imageA = null;
                    }
                    imageA = new Bitmap(openFileDialog.FileName);
                    pictureBox1.Image = imageA;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private async void clearImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentDevice != null)
            {
                await StopCamera();
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

            if (imageA != null)
            {
                imageA.Dispose();
                imageA = null;
            }
            if (imageB != null)
            {
                imageB.Dispose();
                imageB = null;
            }

            isSubtractionMode = false;

            this.Size = new Size(713, 406);
            imageSubtractionToolStripMenuItem.Text = "Image Subtraction";

            MessageBox.Show("All images cleared.", "Clear",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void basicCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;

            Bitmap input = (Bitmap)imageA.Clone();

            if (imageB != null) imageB.Dispose();
            imageB = new Bitmap(input);

            pictureBox2.Image = imageB;
        }

        private void greyscaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isNoImageA()) return;

            Bitmap input = (Bitmap)imageA.Clone();

            if (imageB != null) imageB.Dispose();
            imageB = new Bitmap(input);

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

            Bitmap input = (Bitmap)imageA.Clone();

            if (imageB != null) imageB.Dispose();
            imageB = new Bitmap(input);

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

            Bitmap input = (Bitmap)imageA.Clone();

            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    Color pixel = input.GetPixel(x, y);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    hist[grey]++;
                }
            }

            int max = 0;
            foreach (int val in hist)
                if (val > max) max = val;

            if (imageB != null) imageB.Dispose();
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

            Bitmap input;
            lock (imageALock)
            {
                input = (Bitmap)imageA.Clone();
            }

            Bitmap tempImageA = new Bitmap(input);

            if (imageB != null) imageB.Dispose();
            imageB = new Bitmap(tempImageA.Width, tempImageA.Height);

            for (int x = 0; x < tempImageA.Width; x++)
            {
                for (int y = 0; y < tempImageA.Height; y++)
                {
                    Color pixel = tempImageA.GetPixel(x, y);
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
                pictureBox3.Show();
                this.Size = new Size(713, 815);
            }
            else
            {
                isSubtractionMode = false;
                imageSubtractionToolStripMenuItem.Text = "Image Subtraction";
                pictureBox3.Hide();
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

            isLiveSubtraction = true;

            if (imageA != null)
            {
                ProcessFrame(imageA);
            }
        }

        private void ProcessFrame(Bitmap frame)
        {
            if (imageB == null) return;

            Bitmap resizedBackground = new Bitmap(imageB, frame.Width, frame.Height);
            Bitmap outputFrame = new Bitmap(frame.Width, frame.Height);

            for (int x = 0; x < frame.Width; x++)
            {
                for (int y = 0; y < frame.Height; y++)
                {
                    Color pixel = frame.GetPixel(x, y);
                    float hue = pixel.GetHue();
                    float sat = pixel.GetSaturation();
                    float bri = pixel.GetBrightness();

                    bool isGreen = (hue >= 40 && hue <= 180 && sat > 0.15 && bri > 0.15);

                    if (isGreen)
                        outputFrame.SetPixel(x, y, resizedBackground.GetPixel(x, y));
                    else
                        outputFrame.SetPixel(x, y, pixel);
                }
            }

            if (pictureBox3.IsHandleCreated)
            {
                pictureBox3.Invoke((MethodInvoker)delegate
                {
                    if (pictureBox3.Image != null) pictureBox3.Image.Dispose();
                    pictureBox3.Image = outputFrame;
                });
            }

            resizedBackground.Dispose();
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
