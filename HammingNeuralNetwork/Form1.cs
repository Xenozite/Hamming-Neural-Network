using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace HammingNeuralNetwork
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private Bitmap bitmap;
        private bool paintAvaliable;
        private string imagesPath;
        private string currentImage;
        private List<string> imagesList;
        private NeuralNetwork neuralNetwork;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            paintAvaliable = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            paintAvaliable = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (paintAvaliable)
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    graphics.FillRectangle(Brushes.Black, e.X, e.Y, (float)numericUpDown1.Value, (float)numericUpDown2.Value);
                }
                else
                {
                    graphics.FillEllipse(Brushes.Black, e.X, e.Y, (float)numericUpDown1.Value, (float)numericUpDown2.Value);
                }
                pictureBox1.Refresh();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(200, 200);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            pictureBox1.Image = bitmap;
            comboBox1.SelectedIndex = 0;
            imagesList = new List<string>();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            graphics.Clear(Color.White);
            pictureBox1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            uint noisePixelsAmount = (uint)numericUpDown3.Value * 400;
            int[] pixelsX = new int[noisePixelsAmount];
            int[] pixelsY = new int[noisePixelsAmount];
            int pixelX;
            int pixelY;
            double Y;
            for (int i = 0; i < noisePixelsAmount - 1; i++)
            {
                Mark:
                pixelX = random.Next(0, 200);
                pixelY = random.Next(0, 200);
                bool flag = false;
                for (int j = 0; j < noisePixelsAmount - 1; j++)
                {
                    if (pixelsX[j] == pixelX && pixelsY[j] == pixelY)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    goto Mark;
                }
                else
                {
                    pixelsX[i] = pixelX;
                    pixelsY[i] = pixelY;
                }
            }
            for (int i = 0; i < noisePixelsAmount - 1; i++)
            {
                graphics.FillRectangle(Brushes.Black, pixelsX[i], pixelsY[i], 1, 1);
            }
            pictureBox1.Refresh();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            uint noisePixelsAmount = (uint)numericUpDown3.Value * 400;
            int[] pixelsX = new int[noisePixelsAmount];
            int[] pixelsY = new int[noisePixelsAmount];
            int pixelX;
            int pixelY;
            double Y;
            for (int i = 0; i < noisePixelsAmount - 1; i++)
            {
                Mark:
                pixelX = random.Next(0, 200);
                pixelY = random.Next(0, 200);
                bool flag = false;
                for (int j = 0; j < noisePixelsAmount - 1; j++)
                {
                    if (pixelsX[j] == pixelX && pixelsY[j] == pixelY)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    goto Mark;
                }
                else
                {
                    pixelsX[i] = pixelX;
                    pixelsY[i] = pixelY;
                }
            }
            for (int i = 0; i < noisePixelsAmount - 1; i++)
            {
                // For the formula look at the article
                // "HAMMING NETWORK AND ITS APPLICATION TO SOLVE THE PROBLEM OF RECOGNITION OF SIGNATURES" || A.A. Ezhov, A.S. Novikov
                Y = 0.3 * bitmap.GetPixel(pixelsX[i], pixelsY[i]).R + 0.59 * bitmap.GetPixel(pixelsX[i], pixelsY[i]).G + 0.11 * bitmap.GetPixel(pixelsX[i], pixelsY[i]).B;
                if (Y > 90)
                {
                    graphics.FillRectangle(Brushes.Black, pixelsX[i], pixelsY[i], 1, 1);
                }
                else
                {
                    graphics.FillRectangle(Brushes.White, pixelsX[i], pixelsY[i], 1, 1);
                }
            }
            pictureBox1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                imagesPath = folderBrowserDialog1.SelectedPath;
                textBox1.Text = imagesPath;
                ParseImages();
            }
        }

        private void ParseImages()
        {
            imagesList.Clear();
            imagesList.AddRange(Directory.GetFiles(imagesPath));
            foreach (string line in imagesList.ToArray())
            {
                if (!line.Contains(".bmp"))
                {
                    imagesList.Remove(line);
                }
            }
            ImageAmount.Text = imagesList.Count.ToString();
            ImagesAmount.Text = imagesList.Count.ToString();
            SynapsesAmount.Text = (imagesList.Count * 40000).ToString();
            if (imagesList.Count != 0)
            {
                CurrentImage.Text = "1";
                bitmap = new Bitmap(imagesList[0]);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                currentImage = imagesList[0];
            }
            else
            {
                CurrentImage.Text = "0";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int currentIndex = imagesList.IndexOf(currentImage);
            if (currentIndex > 0)
            {
                currentIndex--;
                CurrentImage.Text = (currentIndex + 1).ToString();
                bitmap = new Bitmap(imagesList[currentIndex]);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                currentImage = imagesList[currentIndex];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int currentIndex = imagesList.IndexOf(currentImage);
            if (currentIndex < imagesList.Count - 1)
            {
                currentIndex++
;
                CurrentImage.Text = (currentIndex + 1).ToString();
                bitmap = new Bitmap(imagesList[currentIndex]);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                currentImage = imagesList[currentIndex];
            }
        }

        private int[] ImageToVector(Image image)
        {
            int[] binaryVector = new int[40000];
            int counter = 0;
            double Y = 0;
            Bitmap buffer = new Bitmap(image);
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    // For the formula look at the article
                    // "HAMMING NETWORK AND ITS APPLICATION TO SOLVE THE PROBLEM OF RECOGNITION OF SIGNATURES" || A.A. Ezhov, A.S. Novikov
                    Y = 0.3 * buffer.GetPixel(j, i).R + 0.59 * buffer.GetPixel(j, i).G + 0.11 * buffer.GetPixel(j, i).B;
                    if (Y > 90)
                    {
                        binaryVector[counter] = -1;
                    }
                    else
                    {
                        binaryVector[counter] = 1;
                    }
                    counter++;
                }
            }
            return binaryVector;
        }

        private double RampFunction(double X)
        {
            if (X <= 0.0)
            {
                return 0.0;
            }
            else if (X >= 20000.0)
            {
                return 20000.0;
            }
            else
            {
                return X;
            }
        }

        private double[] VectorSubstraction(double[] P, double[] Q, int Length)
        {
            double[] R = new double[Length];
            for (int i = 0; i < Length; i++)
            {
                R[i] = P[i] - Q[i];
            }
            return R;
        }

        private double VectorNorm(double[] P, int Length)
        {
            double R = 0.0;
            for (int i = 0; i < Length; i++)
            {
                R += Math.Pow(P[i], 2);
            }
            return R;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (imagesList.Count != 0 & neuralNetwork != null)
            {
                int[] binaryVector = ImageToVector(bitmap);
                ////////////////////////// Frist layer //////////////////////////
                for (int i = 0; i < neuralNetwork.m; i++)
                {
                    neuralNetwork.FirstLayer[i].S = 0.0;
                    for (int j = 0; j < 40000; j++)
                    {
                        neuralNetwork.FirstLayer[i].S += (neuralNetwork.FirstLayer[i].Weights[j]) * binaryVector[j];
                    }
                    neuralNetwork.FirstLayer[i].S += neuralNetwork.T;
                    // No ramp function needed here
                    neuralNetwork.FirstLayer[i].Y = neuralNetwork.FirstLayer[i].S; 
                }
                ////////////////////////// Second layer //////////////////////////
                double[] backUpY = new double[neuralNetwork.m];
                double[] newY = new double[neuralNetwork.m];
                for (int i = 0; i < neuralNetwork.m; i++)
                {
                    neuralNetwork.SecondLayer[i] = new Neuron();
                    neuralNetwork.SecondLayer[i].Y = neuralNetwork.FirstLayer[i].Y;
                    backUpY[i] = neuralNetwork.FirstLayer[i].Y;
                }
                bool flag;
                int counter = 0;
                do
                {
                    for (int i = 0; i < neuralNetwork.m; i++)
                    {
                        neuralNetwork.SecondLayer[i].S = 0.0;
                        for (int j = 0; j < neuralNetwork.m; j++)
                        {
                            if (i != j)
                            {
                                neuralNetwork.SecondLayer[i].S += backUpY[j];
                            }
                        }
                        neuralNetwork.SecondLayer[i].S = backUpY[i] - neuralNetwork.E * neuralNetwork.SecondLayer[i].S;
                        // Apply ramp function on second layer neurons exits
                        neuralNetwork.SecondLayer[i].Y = RampFunction(neuralNetwork.SecondLayer[i].S); 
                        newY[i] = neuralNetwork.SecondLayer[i].Y;
                    }
                    flag = VectorNorm(VectorSubstraction(newY, backUpY, neuralNetwork.m), neuralNetwork.m) > neuralNetwork.Emax;
                    if (flag)
                    {
                        for (int i = 0; i < neuralNetwork.m; i++)
                        {
                            backUpY[i] = newY[i];
                        }
                    }
                    counter++;
                } while (flag);
                ////////////////////////// Determining the index of the recognized image //////////////////////////
                double maxValue = double.MinValue;
                int index = 0;
                for (int i = 0; i < neuralNetwork.m; i++)
                {
                    if (neuralNetwork.SecondLayer[i].Y > maxValue)
                    {
                        maxValue = neuralNetwork.SecondLayer[i].Y;
                        index = i;
                    }
                }
                pictureBox2.Load(imagesList.ToArray()[index]);
                IterationsAmount.Text = counter.ToString();
                ////////////////////////// Build a histogram of the state of neurons //////////////////////////
                Series series = new Series();
                for (int i = 0; i < neuralNetwork.m; i++)
                {
                    series.Points.AddXY(i+1, newY[i]);
                }
                chart1.Series.Clear();
                chart1.Series.Add(series);
            }
            else
            {
                MessageBox.Show("Images not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (imagesList.Count != 0)
            {
                neuralNetwork = new NeuralNetwork();
                neuralNetwork.n = 40000;
                neuralNetwork.m = imagesList.Count;
                neuralNetwork.T = 20000.0;
                neuralNetwork.E = 1.0 / imagesList.Count;
                neuralNetwork.Emax = 0.1;
                neuralNetwork.FirstLayer = new Neuron[imagesList.Count];
                neuralNetwork.SecondLayer = new Neuron[imagesList.Count];
                for (int i = 0; i < neuralNetwork.m; i++)
                {
                    Image image = new Bitmap(imagesList.ToArray()[i]);
                    int[] binaryVector = ImageToVector(image);
                    image.Dispose();
                    neuralNetwork.FirstLayer[i] = new Neuron();
                    neuralNetwork.FirstLayer[i].S = 0.0;
                    neuralNetwork.FirstLayer[i].Y = 0.0;
                    neuralNetwork.FirstLayer[i].Weights = new double[40000];
                    for (int j = 0; j < 40000; j++)
                    {
                        neuralNetwork.FirstLayer[i].Weights[j] = binaryVector[j] / 2.0;
                    } 
                }
                MessageBox.Show("Done!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Images not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "BMP Image | *.bmp";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialog1.FileName);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
            }
        }


    }
}
