namespace VillarazoImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap imageA, imageB, colorgreen;
        public Form1()
        {
            InitializeComponent();
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

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
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
    }
}
