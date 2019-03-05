using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace yazlab2_2
{
    public partial class cizim : Form
    {
        Pen myPen = new Pen(Color.DarkSlateGray); //LABIRENT ICIN
        Graphics g = null;

        static int baslangic_x = 80;
        static int baslangic_y = 80;
        static double n = 3;
        public static int node_sayaci = 0;

        static int start_x, start_y;

        public static int[] agac;
        public static int[] preOrder;
        public static int kok = 4;
        public cizim()
        {
            InitializeComponent();
            start_x = baslangic_x;
            start_y = baslangic_y;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            agac = new int[7] { 4,2,6,1,3,5,7};
            //agac[0] = 3;
            //agac[1] = 7;
            //agac[2] = 6;
            //agac[3] = 15;
            //agac[4] = 18;
            //agac[5] = 9;

            preOrder = new int[7];


            myPen.Width = 3;
            g = canvas.CreateGraphics();

            int x = 15;
            int h = 25;
            int m = 60;
            System.Drawing.Graphics grafiknesne;
            grafiknesne = this.CreateGraphics();
            int sayac = 0;
            int sayac1 = 1;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < Math.Pow(2, i); j++)
                {
                    g.DrawEllipse(myPen, canvas.Width / 2 - h - sayac * (x + h) + ((4 * (x + h)) / sayac1) * j, 50 + (m + 2 * h) * i, 50, 50);
                    if (i == 0)
                    {
                        sayac++;
                    }

                }
                sayac++;
                if (i > 0)
                {
                    sayac1++;
                }
            }
            int syc = 2;
            int baslangic_x = canvas.Width / 2;
            int baslangic_y = 50 + h + h;
            int baslangic_x_1 = canvas.Width / 2;
            int baslangic_y_1 = 50 + h + h;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < Math.Pow(2, (i + 1)); j++)
                {
                    if (j % 2 == 0)
                    {
                        g.DrawLine(myPen, baslangic_x + (2 * (x + h)) * j, baslangic_y, baslangic_x - syc * (x + h) + (2 * (x + h)) * j, baslangic_y + m);
                    }

                    if (j % 2 == 1)
                    {
                        g.DrawLine(myPen, baslangic_x_1 - (2 * (x + h)) * (j - 1), baslangic_y_1, baslangic_x_1 + syc * (x + h) - (2 * (x + h)) * (j - 1), baslangic_y_1 + m);
                    }

                }
                baslangic_x = baslangic_x - syc * (x + h);
                baslangic_y = baslangic_y + m + 2 * h;
                baslangic_x_1 = baslangic_x_1 + syc * (x + h);
                baslangic_y_1 = baslangic_y_1 + m + 2 * h;

                syc--;
            }

            Font yazi = new Font("Georgia", 18, FontStyle.Bold);
            Brush firca = new SolidBrush(Color.White);
           // g.DrawString(kok.ToString(), yazi, firca, canvas.Width / 2 - 10, 50 + 5);


            Tree agacS = new Tree();

            //agacS.insert(kok);
            for (int i = 0; i < agac.Length; i++)
            {
                agacS.insert(agac[i]);
            }
            textBox1.Text = textBox1.Text + "\nAgacın PreOrder Dolasılması : ";
            agacS.preOrder(agacS.getRoot());

            for (int i = 0; i < preOrder.Length; i++)
            {
                textBox1.Text = textBox1.Text + "  " + preOrder[i] + "  ";
            }
            //b.display();

            //g.DrawString(kok.ToString(), yazi, firca, canvas.Width / 2 - 10, 50 + 5);
            //for (int i = 1; i < agac.Length; i++)
            //{

            //    if (preOrder[i] < preOrder[i - 1])
            //    {
            //        g.DrawString(preOrder[i].ToString(), yazi, firca, canvas.Width / 2 - 10-(x+h)*(i+1), 50 + 5+(2*h+m)*i);
            //    }

            //}
         
            int sy = 0;
            for (int i = 0; i < preOrder.Length; i++)
            {
                if (i == 0)
                {
                    g.DrawString(preOrder[i].ToString(), yazi, firca, canvas.Width / 2 - 10, 50 + 5 + (2 * h + m) * i);
                    

                }
                else if (preOrder[i] < preOrder[i - 1] && i!=0)
                {
                                     
                    g.DrawString(preOrder[i].ToString(), yazi, firca, canvas.Width / 2 - 10 - (x + h) * (sy+2), 50 + 5 + (2 * h + m) * i);
                    sy++;
                    if (sy == 2)
                    {  
                        i++;
                        g.DrawString(preOrder[i].ToString(), yazi, firca, canvas.Width / 2 - 10 - (x + h) * (sy-1) , 50 + 5 + (2 * h + m)*sy);
                        sy = 0;
                    }
                   
                }
                
            }

            g.DrawString(preOrder[4].ToString(), yazi, firca, canvas.Width / 2 - 10 + (x + h) * 2, 50 + 5 + (2 * h + m));
            g.DrawString(preOrder[5].ToString(), yazi, firca, canvas.Width / 2 - 10 + (x + h), 50 + 5 + (2 * h + m) * 2);
            g.DrawString(preOrder[6].ToString(), yazi, firca, canvas.Width / 2 - 10 + (x + h) * 3, 50 + 5 + (2 * h + m)*2);





        }
    }
}
