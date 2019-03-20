using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Puzzle
{
    public partial class Form1 : Form
    {

        ArrayList images = new ArrayList();
        public Form1()
        {

            
            
            InitializeComponent();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls)
                b.Enabled = true;
            
            Image original = Image.FromFile(@"C:\Users\Alp Eren SUKAS\Desktop\levi.png");

            cropImageTomages(original, 484, 484);

            AddImagesButtons(images);

        }

        private void AddImagesButtons(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            arr = suffle(arr);

            foreach(Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }

        }

        private int[] suffle(int[] arr)
        {
            Random rand = new Random();
            arr = arr.OrderBy(x => rand.Next()).ToArray();

            return arr;
        }

        private void cropImageTomages(Image original, int w, int h)
        {
            Bitmap bmp = new Bitmap(w, h);
            Graphics graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(original, 0, 0, w, h);
            graphic.Dispose();

            int movr = 0, movd = 0;

            for (int x = 0; x < 16; x++)
            {
                Bitmap piece = new Bitmap(121, 121);

                for (int i = 0; i < 121; i++)
                {
                    for (int j = 0; j < 121; j++)
                    {
                        piece.SetPixel(i, j, bmp.GetPixel(i + movr, j + movd));
                    }

                }
                
                images.Add(piece);
                movr += 121;

                if (movr == 484)
                {
                    movr = 0;
                    movd += 121;
                }
            }

            

        }
    }
}


