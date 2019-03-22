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
        Button b1;
        Button b2;
        Image original;
        int count = 1;
        int score = 100;
        public Form1()
        {

            
            
            InitializeComponent();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            foreach (Button b in panel1.Controls)
                b.Enabled = true;
            count--;
            if (count < 0)
            {
                score -= 10;
                textBox1.Text = Convert.ToString(score);
                if (score == 0)
                {
                    MessageBox.Show("KAYBETTİN!");
                    Close();
                }
            }
            //Image original = Image.FromFile(@"C:\Users\Alp Eren SUKAS\Desktop\levi.png");

            cropImageTomages(original, 484, 484);

            AddImagesButtons(images);

        }

        private void AddImagesButtons(ArrayList images)
        {
            int i = 0;
            int[] arr = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            arr = shuffle(arr);

            foreach(Button b in panel1.Controls)
            {
                if (i < arr.Length)
                {
                    b.Image = (Image)images[arr[i]];
                    i++;
                }
            }

        }

        private int[] shuffle(int[] arr)
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

        private void button1_Click(object sender, EventArgs e)
        {


            if (b1 == null)
            {
                b1 = (Button)sender;
            }

            else
            {
                b2 = (Button)sender;
            }

            if(b1!=null && b2 != null)
            {
                MoveButton(b1, b2);
            }
            
        }

        private void MoveButton(Button b1, Button b2)
        {
            Point swap = b1.Location;
            b1.Location = b2.Location;
            b2.Location = swap;
            

            b1 = null;
            b2 = null;

            CheckValid();

        }

        private void CheckValid()
        {
            int count = 0, index;
            foreach (Button btn in panel1.Controls)
            {
                index = (btn.Location.Y / 121) * 4 + btn.Location.X / 121;
                if (images[index] == btn.Image)
                    count++;
            }
            if (count == 16)
                MessageBox.Show("KAZANDIN!");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG)|*.BMP;*.JPG;*.GIF;*.PNG;*.JPEG|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                original = Image.FromFile(fileName);
            }

            textBox1.Text = Convert.ToString(score);
        }
    }
}


