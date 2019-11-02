using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image_Reassambler
{
    public partial class Form1 : Form
    {
        Bitmap image;
        Bitmap outImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void SelectImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                image = new Bitmap(openFileDialog1.OpenFile());
                pictureBox1.Image = image;
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "jpeg files(.jpeg)|*.jpeg|All files (*.*)|*.*";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap outimage = new Bitmap(outImage.Width, outImage.Height);
                    outimage = outImage;
                    outImage.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                catch (Exception exception)
                {
                    textBox3.Text = exception.ToString();
                }
            }
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            pictureBox2.Image = image;
            string lenghtstring = "";
            for (int pre = 0; pre < 32; pre++)
            {
                if(image.GetPixel(pre,0) == Color.FromArgb(255, 0, 0,  0))
                {
                    lenghtstring += "1";
                }
                if (image.GetPixel(pre , 0) == Color.FromArgb(255,255,255,255))
                {
                    if (lenghtstring.Contains("1"))
                    {
                        lenghtstring += "0";
                    }
                }
            }
            
            int width = Convert.ToInt32(lenghtstring, 2);
            int height = (int)(image.Width / width * (image.Height)); //+ (image.Width - 32) / width);
            textBox3.Text = lenghtstring + " " + lenghtstring.Length + " " + width+ " " + height;
            int lenght = 32;
            int imheight = 0;
            outImage = new Bitmap(width,height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (lenght < 32768)
                    {
                        outImage.SetPixel(x, y, image.GetPixel(lenght, imheight));
                        lenght++;
                    }else
                    {
                        imheight++;
                        lenght = 0;
                    }
                }
            }
            pictureBox1.Image = outImage;
        }

    }
}
