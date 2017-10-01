using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace XoayAnh
{
    public partial class Form1 : Form
    {
        Image img;
        int angle;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            img = Image.FromFile(Application.StartupPath + @"\chiecnon.png");
        }
        
        //Hàm vẽ trên form, vẽ theo ông trên youtube: https://www.youtube.com/watch?v=S9uSKO-BIgs
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int newWidth = 0, newHeight = 0;
            //angle = (int)(angle * 3.6); // nếu sử dụng trackbar thì xài dòng này

            Bitmap bmp = new Bitmap(img.Width, img.Height);
            //
            if (angle <= 90)
            {
                newWidth = (int)(bmp.Width * Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * Math.Sin(2 * Math.PI * angle / 360));
                newHeight = (int)(bmp.Width * Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * Math.Sin(2 * Math.PI * angle / 360));
            }
            else if (angle > 90 && angle <= 180)
            {
                newWidth = (int)(bmp.Width * -Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * Math.Sin(2 * Math.PI * angle / 360));
                newHeight = (int)(bmp.Width * -Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * Math.Sin(2 * Math.PI * angle / 360));
            }
            else if (angle > 180 && angle <= 270)
            {
                newWidth = (int)(bmp.Width * -Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * -Math.Sin(2 * Math.PI * angle / 360));
                newHeight = (int)(bmp.Width * -Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * -Math.Sin(2 * Math.PI * angle / 360));
            }
            else if (angle > 270 && angle <= 360)
            {
                newWidth = (int)(bmp.Width * Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * -Math.Sin(2 * Math.PI * angle / 360));
                newHeight = (int)(bmp.Width * Math.Cos(2 * Math.PI * angle / 360) + bmp.Height * -Math.Sin(2 * Math.PI * angle / 360));
            }

            this.Text = angle.ToString() + "%% nW: " + newWidth.ToString() + "%% nH: " + newHeight.ToString();

            Bitmap bm = new Bitmap(newWidth, newHeight);
            Graphics g = Graphics.FromImage(bm);
            
            // dời đến tâm của hình
            g.TranslateTransform(newWidth / 6, newHeight / 6);
            g.RotateTransform(angle); // xoay
            //dời lại
            g.TranslateTransform(-img.Width / 6, -img.Height / 6);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic; // hình như là độ phân giải


            g.DrawImage(img, 0, 0);
            //Chuyen goc toa do den tam bức hình
            e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
            e.Graphics.DrawImage(bm, -bm.Width / 6, -bm.Height / 6); // dời lại
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            //angle = trackBar1.Value;
            //Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 100; // làm cho timer nhanh hay chậm
            if(angle == 360)
            {
                angle = 0;
            }
            else
                angle +=15;
            pictureBox1.Invalidate(); // vẽ lại trên picturebox
            Invalidate(); // vẽ lại trên form
        }

        //Hàm vẽ trên picturebox
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            Graphics g = e.Graphics;
            
            g.TranslateTransform(pictureBox1.Width / 2, pictureBox1.Height / 2);
            g.RotateTransform(angle);
            g.TranslateTransform(-pictureBox1.Width / 2 , -pictureBox1.Height / 2);
            g.DrawImage(img, 0, 0);
            
        }
        //Bấm pic để dừng hoặc quay
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }
    }
}