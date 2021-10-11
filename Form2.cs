using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/* -----------------------------
This Project is produced by:
-------------------------------
Eng.Muhammad Gamal Abd El-Hady
Eng.Muhammad Montaser Ahmed
-------------------------------*/

namespace CP
{
    public partial class ComGraph : Form
    {
        public ComGraph()
        {
            InitializeComponent();
        }
        
        int x1=0, x2=0, y1=0, y2=0, r=0;
        int dir = 1, dir2 = -1;
        void DDA(int X1, int Y1, int X2, int Y2)
        {
            Bitmap p = new Bitmap(pictureBox2.Width, pictureBox2.Height);

            int dx = X2 - X1;
            int dy = Y2 - Y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));
            float s = steps;
            float xi = dx / s;
            float yi = dy / s;
            float x = X1;
            float y = Y1;
            p.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.White);
            for (int i = 0; i < steps; i++)
            {
                x += xi;
                y += yi;
                p.SetPixel((int)Math.Round(x), (int)Math.Round(y), Color.White);
            }
            pictureBox2.Image = p;
        }

        void Circle(int xc, int yc, int r)
        {
            Bitmap I = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            int x = r;
            int y = 0;
            int p0 = 1 - r;
            while (x >= y)
            {
                I.SetPixel(x + xc, y + yc, Color.White);
                I.SetPixel(y + xc, x + yc, Color.White);
                I.SetPixel(-x + xc, y + yc, Color.White);
                I.SetPixel(-y + xc, x + yc, Color.White);
                I.SetPixel(-x + xc, -y + yc, Color.Red);
                I.SetPixel(-y + xc, -x + yc, Color.White);
                I.SetPixel(x + xc, -y + yc, Color.White);
                I.SetPixel(y + xc, -x + yc, Color.White);
                if (p0 <= 0)
                {
                    p0 = p0 + 2 * (y + 1) + 1;
                    y++;
                }
                else
                {
                    p0 = p0 + 2 * (y + 1) - 2 * (x - 1) + 1;
                    y++;
                    x--;
                }
            }
            pictureBox2.Image = I;
        }

        void Ellipse(int xc, int yc, int rx, int ry)
        {
            Bitmap M = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            int x = 0; int y = ry;
            double p01 = ry * ry - rx * rx * ry + rx * rx / 4;
            int m = 2 * ry * ry * x;
            int n = 2 * rx * rx * y;
            do
            {
                M.SetPixel(xc + x, yc + y, Color.White);
                M.SetPixel(xc - x, yc + y, Color.White);
                M.SetPixel(xc - x, yc - y, Color.White);
                M.SetPixel(xc + x, yc - y, Color.White);
                if (p01 < 0)
                {
                    x++;
                    m = 2 * ry * ry * x;
                    n = 2 * rx * rx * y;
                    p01 = p01 + ry * ry * (2 * x + 3);
                }
                else
                {
                    x++; y--;
                    m = 2 * ry * ry * x;
                    n = 2 * rx * rx * y;
                    p01 = p01 + ry * ry * (2 * x + 3) + rx * rx * (-2 * y + 2);
                }
            } while (m < n);

            double p02 = ry * ry * (x + 0.5) * (x + 0.5) + rx * rx * (y - 1) * (y - 1) - rx * rx * ry * ry;
            do
            {
                M.SetPixel(xc + x, yc + y, Color.White);
                M.SetPixel(xc - x, yc + y, Color.White);
                M.SetPixel(xc - x, yc - y, Color.White);
                M.SetPixel(xc + x, yc - y, Color.White);
                if (p02 < 0)
                {
                    y--; x++;
                    p02 = p02 + ry * ry * (2 * x + 2) + rx * rx * (-2 * y + 3);

                }
                else
                {
                    y--;
                    p02 -= rx * rx * (2 * y - 3);
                }
            } while (y >= 0);
            pictureBox2.Image = M;
        }
        //--------------------MATRIXES FUNCTION--------------
        double[,] matrix33_X_matrix33(double[,] arr1, double[,] arr2)
        {
            double[,] res = new double[3, 3];

            for (int i = 0; i < 9; i++)
            {

                for (int col = 0; col < 3; col++)
                {
                    res[i / 3, i % 3] += arr1[i / 3, col] * arr2[col, i % 3];
                }
                
            }

            return res;
        }
        double[] matrix33_X_matrix31(double[,] arr1, double[] arr2)
        {
            double[] res = new double[3];

            for (int i = 0; i < 9; i++)
            {
                res[i / 3] += arr1[i / 3, i % 3] * arr2[i % 3];
            }

            return res;
        }
        double[,] tras_to_zero(double x, double y)
        {
            double[,] trans_to_zero ={
                                 {1,0,-1*x},
                                 {0,1,-1*y},
                                 {0,0,1}
                                  };
            return trans_to_zero;
        }

        //------------------DRAW FUNCTION-------------------
        public void draw_point(int x, int y, PictureBox ppp)
        {
            try
            {
                if (x < 0 || x > ppp.Width || y < 0 || y > ppp.Height)
                {
                    return;
                }
                ((Bitmap)ppp.Image).SetPixel(x, y, Color.Red);
            }
            catch (Exception) { }
        }
        List<List<int>> DATA = new List<List<int>>();
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        // DDA - Line
        private void DDA_btn1_Click(object sender, EventArgs e)
        {
            try
            {
                x1 = int.Parse(txtbx_X1.Text);
                y1 = int.Parse(txtbx_Y1.Text);
                x2 = int.Parse(txtbx_X2.Text);
                y2 = int.Parse(txtbx_Y2.Text);
                DDA(x1, y1, x2, y2);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                    "PLZ, Input another values for x-axis and y-axis";

                MessageBox.Show(message);
            }
        }
        // Ellipse - Midpoint
        private void Ellipse_btn4_Click(object sender, EventArgs e)
        {
            try
            {
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int rx = int.Parse(txtbx_X2.Text);
                int ry = int.Parse(txtbx_Y2.Text);
                Ellipse(xc, yc, rx, ry);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                       "PLZ, Input another values for x-axis and y-axis";

                MessageBox.Show(message);
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {

        }
        // Close Button
        private void close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (panel2.Height == 96)
            {
                panel2.Height = 24;
            }
            else
            {
                panel2.Height = 96;
            }
        }

        private void Form2(object sender, EventArgs e)
        {
            panel2.Height = 24;
            panel3.Height = 24;
            panel4.Height = 24;
            panel5.Height = 24;

        }

        private void scl_Click(object sender, EventArgs e)
        {
            if (panel4.Height == 96)
            {
                panel4.Height = 24;
            }
            else
            {
                panel4.Height = 96;
            }
        }
        // Bresenham - Line
        private void Bresenham_btn2_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap b = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                int x1 = int.Parse(txtbx_X1.Text);
                int y1 = int.Parse(txtbx_Y1.Text);
                int x2 = int.Parse(txtbx_X2.Text);
                int y2 = int.Parse(txtbx_Y2.Text);
                int dx = x2 - x1;
                int dy = y2 - y1;
                int x = x1;
                int y = y1;
                int p = 2 * dy - dx;
                for (int i = x1; i <= x2; i++)
                {
                    b.SetPixel(x, y, Color.White);
                    x++;
                    if (p < 0)
                    {
                        p += 2 * dy;
                    }
                    else
                    {
                        p = p + 2 * dy - 2 * dx;
                        y++;
                    }
                }
                pictureBox2.Image = b;
            }
            catch
            {
                string message = "Invalid Values !! \n " +
                    "PLZ, Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Circle - Midpoint
        private void Circle_btn3_Click(object sender, EventArgs e)
        {
            try
            {
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int r = int.Parse(txtbx_RC.Text);
                Circle(xc, yc, r);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                    "PLZ, Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Clear Button
        private void clrbtn_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Image = null;
        }
        // Tranlate - Ellipse
        private void ebtn_Click(object sender, EventArgs e)
        {
            try
            {
                int tx = int.Parse(txtbx_TX.Text);
                int ty = int.Parse(txtbx_TY.Text);
                x1 += tx;
                y1 += ty;
                Ellipse(x1, y1, x2, y2);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                    "PLZ, Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Rotate - Circle
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                double ang = double.Parse(txtbx_Angle.Text);
                double s1 = Math.Cos(ang * Math.PI / 180);
                double s2 = Math.Sin(ang * Math.PI / 180);
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int r = int.Parse(txtbx_RC.Text);
                double c1 = (int)Math.Round((xc * s1) - (yc * s2));
                double c2 = (int)Math.Round((yc * s1) + (xc * s2));
                Circle((int)c1, (int)c2, r);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Rotate - Ellipse
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int rx = int.Parse(txtbx_X2.Text);
                int ry = int.Parse(txtbx_Y2.Text);
                int temp;
                temp = xc;
                xc = yc;
                yc = temp;
                Ellipse(xc, yc, rx, ry);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        public int Chick(int x, int y, int xmin, int ymin, int xmax, int ymax)
        {
            int inside = 0;   // 0000
            int top = 8;     // 1000
            int bottom = 4;   // 0100
            int right = 2;    // 0010
            int left = 1;  // 0001
            int code = inside;
            if (x < xmin)
                code = code | left;
            if (y < ymin)
                code = code | bottom;
            if (x > xmax)
                code = code | right;
            if (y > ymax)
                code = code | top;
            return code;
        }
        // Line Clipping
        private void clipping_btn9_Click(object sender, EventArgs e)
        {
            try
            {
                int x1 = int.Parse(txtbx_X1.Text);
                int y1 = int.Parse(txtbx_Y1.Text);
                int x2 = int.Parse(txtbx_X2.Text);
                int y2 = int.Parse(txtbx_Y2.Text);
                int xmin = int.Parse(txtbx_Xmin.Text);
                int ymin = int.Parse(txtbx_Ymin.Text);
                int xmax = int.Parse(txtbx_Xmax.Text);
                int ymax = int.Parse(txtbx_Ymax.Text);
                int inside = 0;
                int top = 8;
                int bottom = 4;
                int right = 2;
                int left = 1;
                int code1 = Chick(x1, y1, xmin, ymin, xmax, ymax);
                int code2 = Chick(x2, y2, xmin, ymin, xmax, ymax);
                int codeout;

                while (true)
                {
                    if (code1 == inside & code2 == inside)
                    {
                        DDA(x1, y1, x2, y2);
                        break;
                    }
                    else if ((code1 & code2) != inside)
                    {
                        string message = "Trivial Rejected\nInput another values for x-axis and y-axis";
                        MessageBox.Show(message);
                        break;
                    }
                    else
                    {
                        int x = 0, y = 0;

                        if (code1 == inside)
                            codeout = code1;
                        else
                            codeout = code2;
                        if ((codeout & top) == top)
                        {
                            y = xmax;
                            x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);

                        }
                        else if ((codeout & bottom) == bottom)
                        {
                            y = ymin;
                            x = x1 + (x2 - x1) * (ymax - y1) / (y2 - y1);
                        }
                        else if ((codeout & right) == right)
                        {
                            x = xmax;
                            y = y1 + (y2 - y1) * (xmax - x1) / (x2 - x1);
                        }
                        else if ((codeout & left) == left)
                        {
                            x = xmin;
                            y = y1 + (y2 - y1) * (xmin - x1) / (x2 - x1);
                        }


                        if (codeout == code1)
                        {
                            x1 = x;
                            y1 = y;
                            code1 = Chick(x1, y1, xmin, ymin, xmax, ymax);
                        }
                        else
                        {
                            x2 = x;
                            y2 = y;
                            code2 = Chick(x2, y2, xmin, ymin, xmax, ymax);
                        }
                        DDA(x1, y1, x2, y2);
                    }
                }
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Scale - Line
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                /*int x1 = int.Parse(txtbx_X1.Text);
                int y1 = int.Parse(txtbx_Y1.Text);*/
                int SX = int.Parse(txtbx_SX.Text);
                int SY = int.Parse(txtbx_SY.Text);
                x1 *= SX;
                y1 *= SY;
                /*int x2 = int.Parse(txtbx_X2.Text);
                int y2 = int.Parse(txtbx_Y2.Text);*/
                x2 *= SX;
                y2 *= SY;
                DDA(x1, y1, x2, y2);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Scale - Circle
        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                int x = int.Parse(txtbx_X1.Text);
                int y = int.Parse(txtbx_Y1.Text);
                int r = int.Parse(txtbx_RC.Text);
                int SX = int.Parse(txtbx_SX.Text);
                int SY = int.Parse(txtbx_SY.Text);
                int SR = int.Parse(txtbx_SR.Text);
                int SX1 = x * SX;
                int SY1 = y * SY;
                int SR1 = r * SR;
                Circle(SX1, SY1, SR1);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Scale - Ellipse
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int rx = int.Parse(txtbx_X2.Text);
                int ry = int.Parse(txtbx_Y2.Text);
                int SX = int.Parse(txtbx_SX.Text);
                int SY = int.Parse(txtbx_SY.Text);
                int SR = int.Parse(txtbx_SR.Text);
                int SX1 = xc * SX;
                int SY1 = yc * SY;
                int SR1 = rx * SR;
                int SR2 = ry * SR;
                Ellipse(SX1, SY1, SR1, SR2);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Reflect - Line
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int temp;
                int temp2;
                int x1 = int.Parse(txtbx_X1.Text);
                int y1 = int.Parse(txtbx_Y1.Text);
                int x2 = int.Parse(txtbx_X2.Text);
                int y2 = int.Parse(txtbx_Y2.Text);
                temp = x1;
                x1 = y2;
                temp2 = x2;
                x2 = y1;
                y1 = temp2;
                y2 = temp;
                DDA(x1, y1, x2, y2);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Reflect - Circle
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(txtbx_X1.Text);
                int y = int.Parse(txtbx_Y1.Text);
                int r = int.Parse(txtbx_RC.Text);
                int temp;
                temp = x;
                x = y;
                y = temp;
                Circle(x, y, r);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Reflect - Ellipse
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                int xc = int.Parse(txtbx_X1.Text);
                int yc = int.Parse(txtbx_Y1.Text);
                int rx = int.Parse(txtbx_X2.Text);
                int ry = int.Parse(txtbx_Y2.Text);
                int temp;
                temp = xc;
                xc = yc;
                yc = temp;
                Ellipse(xc, yc, rx, ry);
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Rotate - Line
        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                double ceta = double.Parse(txtbx_Angle.Text) * Math.PI / 180;
                double[,] rotate ={
                      {Math.Cos(ceta),Math.Sin(ceta)*dir,0},
                      {Math.Sin(ceta)*dir2,Math.Cos(ceta),0},
                      {0,0,1}
                      };
                double[,] p1_zer0 = tras_to_zero((x1 + x2) / 2, (y1 + y2) / 2);

                double newddx1 = x1 + p1_zer0[0, 2];
                double newddx2 = x2 + p1_zer0[0, 2];

                double newddy1 = y1 + p1_zer0[1, 2];
                double newddy2 = y2 + p1_zer0[1, 2];

                double[,] come_back = tras_to_zero((x1 + x2) / -2, (y1 + y2) / -2);

                double[,] test = matrix33_X_matrix33(come_back, rotate);
                double[,] res = matrix33_X_matrix33(test, p1_zer0);

                double[] point_1_before = { x1, y1, 1 };
                double[] point_2_before = { x2, y2, 1 };

                double[] point_1_after_rotate = matrix33_X_matrix31(res, point_1_before);
                double[] point_2_after_rotate = matrix33_X_matrix31(res, point_2_before);

                DDA((int)point_1_after_rotate[0], (int)point_1_after_rotate[1], (int)point_2_after_rotate[0], (int)point_2_after_rotate[1]);

                x1 = (int)point_1_after_rotate[0];
                y1 = (int)point_1_after_rotate[1];
                x2 = (int)point_2_after_rotate[0];
                y2 = (int)point_2_after_rotate[1];
            }
            catch
            {
                string message = "Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }
        // Translate - Circle
        private void cbtn_Click(object sender, EventArgs e)
        {
            try
            {
                r = int.Parse(txtbx_RC.Text);
                int tx = int.Parse(txtbx_TX.Text);
                int ty = int.Parse(txtbx_TY.Text);
                x1 += tx;
                y1 += ty;
                Circle(x1, y1, r);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                    "PLZ, Input another values for x-axis and y-axis";
                MessageBox.Show(message);
            }
        }

        private void reflect_Click(object sender, EventArgs e)
        {
            if (panel3.Height == 96)
            {
                panel3.Height = 24;
            }
            else
            {
                panel3.Height = 96;
            }
        }

        private void rotation_Click(object sender, EventArgs e)
        {
            if (panel5.Height == 96)
            {
                panel5.Height = 24;
            }
            else
            {
                panel5.Height = 96;
            }
        }
        // Translate - Line
        private void lbtn_Click(object sender, EventArgs e)
        {
            try
            {
                int tx = int.Parse(txtbx_TX.Text);
                int ty = int.Parse(txtbx_TY.Text);
                x1 += tx;
                y1 += ty;
                x2 += tx;
                y2 += ty;
                DDA(x1, y1, x2, y2);
            }
            catch
            {
                string message = "Invalid Values !! \n" +
                       "PLZ, Input another values for x-axis and y-axis";

                MessageBox.Show(message);
            }
        }
    }
}
