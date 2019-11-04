using System;
using System.IO;
using System.Numerics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Picturre_disassambler
{
    public partial class Form1 : Form
    {
        Bitmap image;
        int maxLenght;
        Bitmap outImage;
        int Lenght = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
            image = new Bitmap(openFileDialog1.OpenFile());
            if (image.Width * image.Height < 32768) maxLenght = image.Width * image.Height; else maxLenght = 32768;
            int height = (int)Math.Ceiling((double)image.Width * image.Height / 32768) + 1;
            outImage = new Bitmap(maxLenght, height);
            pictureBox1.Image = image;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string width = Convert.ToString(image.Width, 2);
            textBox3.Text = width.Length.ToString();

                for(int i = width.Length; i < 32; i++)
                {
                width = "0" + width;
                }
            textBox3.Text = textBox3.Text + " " + width + " " + width.Length + " " + Convert.ToInt32(width, 2) + " " + image.Width * image.Height;

            for (int pre = 0; pre < 32; pre++)
            {
                if (width.Length > pre)
                {
                    if (width[pre] == '1')
                    {
                        outImage.SetPixel(pre, 0, Color.Black);
                    }
                    else if (width[pre] == '0')
                    {
                        outImage.SetPixel( pre, 0, Color.White);
                    }
                }else
                {
                    outImage.SetPixel(pre,0, Color.White);
                }
            }
            Lenght = 32;
            int Height = 0;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                { if (Lenght < 32768)
                    {
                        outImage.SetPixel(Lenght, Height, image.GetPixel(x, y));
                        Lenght++;
                    }
                    else
                    {
                        Lenght = 0;
                        Height++;
                    }
                }
            }
            pictureBox2.Image = outImage;

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Png files(.png)|*.png|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                /*try
                {
                    Bitmap outimage = new Bitmap(outImage.Width, outImage.Height);
                    outimage = outImage;
                    outImage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                catch (Exception exception)
                {
                    textBox3.Text = exception.ToString();
                }*/

                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        outImage.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}