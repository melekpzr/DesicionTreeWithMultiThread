using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace yazlab2_2
{
    public partial class Form1 : Form
    {
        public static string dosya_yolu;
        public static int boyut = 0;
        public static int[] att;
        public static int att_num = 0;
        public static int[,] hasta;
        public static double kucuk_1;
        public static double kucuk_2;
        public static double buyuk_1;
        public static double buyuk_2;
        public static double karar_Entropi = 0.0;
        public static int[] buyuk_ent;
        public static double sonuc = 0.0;
        public static int k = -1;
        public static double[] ozellik_sonuc;
        public static int[] ozellik_bolumleme;
        public static int[] gelen_ozellikler;
        public static int buyuk_ozellik = 0;
        public static int dallandi = 0;
        public static int[,] sol_dallanma_hasta;
        public static int[,] sag_dallanma_hasta;
        public static int sol_dallanma_boyut = 0;
        public static int sag_dallanma_boyut = 0;
        public static int sayac_sol = 0;
        public static int sayac_sag = 0;
        public static Boolean[] kok;



        //-----------------DOSAYADAKİ VERİ SAYISI-------------------

        public static void boyutBelirle()
        {

            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                boyut++;

                yazi = sw.ReadLine();
            }
            //Satır satır okuma işlemini gerçekleştirdik ve ekrana yazdırdık
            //Son satır okunduktan sonra okuma işlemini bitirdik
            sw.Close();
            fs.Close();
            //İşimiz bitince kullandığımız nesneleri iade ettik.
        }



        public Form1()
        {
            InitializeComponent();
        }


        //------------ÖZELLİK SAYISI-------------

        public static void attribute_number()
        {
            FileStream fs = new FileStream(Form1.dosya_yolu, FileMode.Open, FileAccess.Read);
            //Bir file stream nesnesi oluşturuyoruz. 1.parametre dosya yolunu,
            //2.parametre dosyanın açılacağını,
            //3.parametre dosyaya erişimin veri okumak için olacağını gösterir.
            StreamReader sw = new StreamReader(fs);
            //Okuma işlemi için bir StreamReader nesnesi oluşturduk.
            string yazi = sw.ReadLine();

            string[] yollar;
            yollar = yazi.Split(',');
            foreach (string i in yollar) //parcalar dizisiniz tüm elemanları listBox' a eklenir.
            {
                att_num++;

            }
            sw.Close();
            fs.Close();
        }


        //------------GENEL KARAR ENTROPİ--------

        public static double Genel_Entropi(int[,] hayatta_kalma)
        {
            double birlerin_sayisi = 0;
            double ikilerin_sayisi = 0;
            double sonuc = 0.0;

            for (int i = 0; i < boyut; i++)
            {

                if (hayatta_kalma[i, att_num - 1] == 1)
                {
                    birlerin_sayisi++;

                }
                else
                {
                    ikilerin_sayisi++;
                }

            }


            sonuc = (-1) * (birlerin_sayisi / boyut) * (Math.Log((birlerin_sayisi / boyut), 2)) + (-1) * (ikilerin_sayisi / boyut) * (Math.Log((ikilerin_sayisi / boyut), 2));


            return sonuc;
        }


        //--------------------ÖZELLİKLERİN ENTROPİLERİ------------------


        public static double ozellik_entropi(int[,] hasta_dizi, int boyut_dizi, int ozellik, int istenilen_deger)
        {


            kucuk_1 = 0;
            kucuk_2 = 0;
            buyuk_1 = 0;
            buyuk_2 = 0;


            for (int i = 0; i < boyut_dizi; i++)
            {
                if (hasta_dizi[i, ozellik] >= istenilen_deger)
                {
                    if (hasta_dizi[i, att_num - 1] == 1)
                    {
                        buyuk_1++;
                    }

                    if (hasta_dizi[i, att_num - 1] == 2)
                    {
                        buyuk_2++;
                    }
                }


                if (hasta_dizi[i, ozellik] < istenilen_deger)
                {
                    if (hasta_dizi[i, att_num - 1] == 1)
                    {
                        kucuk_1++;
                    }

                    if (hasta_dizi[i, att_num - 1] == 2)
                    {
                        kucuk_2++;
                    }
                }
            }

            double hesapla;

            double kucuk_deger;
            double buyuk_deger;
            kucuk_deger = (((kucuk_1 / (kucuk_1 + kucuk_2)) * Math.Log((kucuk_1 / (kucuk_1 + kucuk_2)), 2)) + ((kucuk_2 / (kucuk_2 + kucuk_1)) * Math.Log((kucuk_2 / (kucuk_2 + kucuk_1)), 2))) * ((kucuk_1 + kucuk_2) / boyut);
            buyuk_deger = (((buyuk_1 / (buyuk_1 + buyuk_2)) * Math.Log((buyuk_1 / (buyuk_1 + buyuk_2)), 2)) + ((buyuk_2 / (buyuk_2 + buyuk_1)) * Math.Log((buyuk_2 / (buyuk_2 + buyuk_1)), 2))) * ((buyuk_1 + buyuk_2) / boyut);
            hesapla = (-1) * (kucuk_deger + buyuk_deger);

            sonuc = karar_Entropi - hesapla;




            dallandi = dallanma(sonuc, ozellik, istenilen_deger);




            return sonuc;
        }


        //-------------------- MAXIMUM BÖLÜMLEME DEĞERİ----------

        public static int dallanma(double sonuc, int gelen_ozellik, int gitti)
        {

            k++;

            double buyuk = 0;
            int j = 0;

            if (sonuc.ToString() == "NaN")
            {
                ozellik_sonuc[k] = 0;
            }

            else
            {
                ozellik_sonuc[k] = sonuc;// 50 ye veya 60 a göre entropi değerleri
                ozellik_bolumleme[k] = gitti; //50 mi 60 mı
                gelen_ozellikler[k] = gelen_ozellik;
            }


            if (k == 3 * (att_num - 1) - 1) //8
            {

                buyuk = ozellik_sonuc[0];

                for (j = 1; j < 9; j++)
                {


                    if (ozellik_sonuc[j] > buyuk)
                    {
                        buyuk = ozellik_sonuc[j];
                        k = j;
                        buyuk_ozellik = gelen_ozellikler[k];
                        

                    }


                }
                
                
                MessageBox.Show("buyuk oz" + buyuk_ozellik);
                kok[buyuk_ozellik] = false;
                
                return ozellik_bolumleme[k];

            }


            return 0;

        }

        //-----------------MATRİS BÖLME----------------------

        public static void yapraklara_ayır(int[,] gelen_hasta_dizi, int gelen_buyuk_ozellik, int dallanan_kok_degeri)
        {


            for (int i = 0; i < boyut; i++)
            {

                if (gelen_hasta_dizi[i, gelen_buyuk_ozellik] < dallanan_kok_degeri)
                {
                    sayac_sol++;
                }
                else
                {
                    sayac_sag++;
                }



                for (int j = 0; j < att_num; j++)
                {

                    if (gelen_hasta_dizi[i, gelen_buyuk_ozellik] < dallanan_kok_degeri)
                    {

                        if (j == gelen_buyuk_ozellik)
                        {
                            sol_dallanma_hasta[sayac_sol - 1, j] = 0;
                        }


                        else
                        {
                            sol_dallanma_hasta[sayac_sol - 1, j] = gelen_hasta_dizi[i, j];
                        }


                    }

                    else
                    {
                        if (j == gelen_buyuk_ozellik)
                        {
                            sag_dallanma_hasta[sayac_sag - 1, j] = 0;
                        }

                        else
                        {
                            sag_dallanma_hasta[sayac_sag - 1, j] = gelen_hasta_dizi[i, j];
                        }

                    }



                }


            }


            for (int i = 0; i < sag_dallanma_boyut; i++)
            {
                for (int j = 0; j < att_num; j++)
                {
                    if (j == gelen_buyuk_ozellik)
                    {
                        sag_dallanma_hasta[i, j] = 0;
                    }


                }
            }






        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //-------------BAŞLANGIÇ----------------



        private void button2_Click(object sender, EventArgs e)
        {



            //Thread tr1 = new Thread(() => ozellik_entropi(hasta, boyut, 0, 50));
            //Thread tr2 = new Thread(() => ozellik_entropi(hasta, boyut, 0, 60));
            //Thread tr3 = new Thread(() => ozellik_entropi(hasta, boyut, 0, 70));
            //Thread tr4 = new Thread(() => ozellik_entropi(hasta, boyut, 1, 62));
            //Thread tr5 = new Thread(() => ozellik_entropi(hasta, boyut, 1, 63));
            //Thread tr6 = new Thread(() => ozellik_entropi(hasta, boyut, 1, 64));
            //Thread tr7 = new Thread(() => ozellik_entropi(hasta, boyut, 2, 5));
            //Thread tr8 = new Thread(() => ozellik_entropi(hasta, boyut, 2, 10));
            //Thread tr9 = new Thread(() => ozellik_entropi(hasta, boyut, 2, 19));

            //tr1.Start();
            //tr2.Start();
            //tr3.Start();
            //tr4.Start();
            //tr5.Start();
            //tr6.Start();
            //tr7.Start();
            //tr8.Start();
            //tr9.Start();

            
            kok = new Boolean[att_num - 1];

            for (int i = 0; i < att_num - 1; i++)
            {
                kok[i] = true;
            }


            double x = ozellik_entropi(hasta, boyut, 0, 50);
            textBox2.Text = x.ToString();
            double y = ozellik_entropi(hasta, boyut, 0, 60);
            textBox3.Text = y.ToString();
            double z = ozellik_entropi(hasta, boyut, 0, 70);
            textBox4.Text = z.ToString();

            double a = ozellik_entropi(hasta, boyut, 1, 62);
            textBox5.Text = a.ToString();
            double b = ozellik_entropi(hasta, boyut, 1, 63);
            textBox6.Text = b.ToString();
            double c = ozellik_entropi(hasta, boyut, 1, 64);
            textBox7.Text = c.ToString();

            double d = ozellik_entropi(hasta, boyut, 2, 5);
            textBox8.Text = d.ToString();
            double g = ozellik_entropi(hasta, boyut, 2, 10);
            textBox9.Text = g.ToString();
            double f = ozellik_entropi(hasta, boyut, 2, 19);
            textBox10.Text = f.ToString();

            MessageBox.Show("1.kök özellik:" + buyuk_ozellik);
            MessageBox.Show("1.dallanan:" + dallandi);





            for (int i = 0; i < boyut; i++)
            {
                if (hasta[i, buyuk_ozellik] < dallandi)
                {
                    sol_dallanma_boyut++;
                }
                else
                {
                    sag_dallanma_boyut++;
                }

            }

            sol_dallanma_hasta = new int[sol_dallanma_boyut, att_num];
            sag_dallanma_hasta = new int[sag_dallanma_boyut, att_num];


            //----------İLK KÖKE GÖRE MATRİSİ AYIRIYOR-----------

            yapraklara_ayır(hasta, buyuk_ozellik, dallandi);



            textBox11.Text = "sol kısım";
            textBox11.Text = textBox11.Text + Environment.NewLine;

            for (int i = 0; i < sol_dallanma_boyut; i++)
            {
                for (int j = 0; j < att_num; j++)
                {

                    textBox11.Text = textBox11.Text + sol_dallanma_hasta[i, j].ToString() + " ";

                }

                textBox11.Text = textBox11.Text + Environment.NewLine;
            }




            k = -1;

            double x_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 0, 50);
            textBox2.Text = x_sol.ToString();
            double y_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 0, 60);
            textBox3.Text = y_sol.ToString();
            double z_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 0, 70);
            textBox4.Text = z_sol.ToString();

            double a_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 1, 62);
            textBox5.Text = a_sol.ToString();
            double b_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 1, 63);
            textBox6.Text = b_sol.ToString();
            double c_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 1, 64);
            textBox7.Text = c_sol.ToString();

            double d_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 2, 5);
            textBox8.Text = d_sol.ToString();
            double g_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 2, 10);
            textBox9.Text = g_sol.ToString();
            double f_sol = ozellik_entropi(sol_dallanma_hasta, sol_dallanma_boyut, 2, 19);
            textBox10.Text = f_sol.ToString();


            MessageBox.Show("2.sol kök özellik:" + buyuk_ozellik);
            MessageBox.Show("2.sol dallanan:" + dallandi);



            textBox12.Text = "sağ kısım";
            textBox12.Text = textBox12.Text + Environment.NewLine;

            for (int i = 0; i < sag_dallanma_boyut; i++)
            {
                for (int j = 0; j < att_num; j++)
                {
                    textBox12.Text = textBox12.Text + sag_dallanma_hasta[i, j].ToString() + " ";
                }
                textBox12.Text = textBox12.Text + Environment.NewLine;
            }


            k = -1;

            double x_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 0, 50);

            textBox2.Text = x_sag.ToString();
            double y_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 0, 60);
            textBox3.Text = y_sag.ToString();
            double z_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 0, 70);
            textBox4.Text = z_sag.ToString();

            double a_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 1, 62);
            textBox5.Text = a_sag.ToString();
            double b_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 1, 63);
            textBox6.Text = b_sag.ToString();
            double c_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 1, 64);
            textBox7.Text = c_sag.ToString();

            double d_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 2, 5);
            textBox8.Text = d_sag.ToString();
            double g_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 2, 10);
            textBox9.Text = g_sag.ToString();
            double f_sag = ozellik_entropi(sol_dallanma_hasta, sag_dallanma_boyut, 2, 19);
            textBox10.Text = f_sag.ToString();


            MessageBox.Show("2. sağ özellik:" + buyuk_ozellik);
            MessageBox.Show("2. sağ dallanan:" + dallandi);
            int art = 0;

            for (int i = 0; i < att_num - 1; i++)
            {
                if (kok[i] != true)
                {
                    art++;
                }
            }


            if (art == 3)
            {
                MessageBox.Show("AĞAÇ OLUŞTU");
            }
            else
            {
                MessageBox.Show("AĞAÇ OLUŞMADI");
            }
        }







        private void button1_Click_1(object sender, EventArgs e)
        {


            Hasta[] hastalar = new Hasta[boyut];
            OpenFileDialog file = new OpenFileDialog();
            file.ShowDialog();
            dosya_yolu = file.FileName;

            boyutBelirle();
            attribute_number();
            hasta = new int[boyut, att_num];

            ozellik_sonuc = new double[3 * att_num];
            ozellik_bolumleme = new int[3 * att_num];
            gelen_ozellikler = new int[3 * att_num];

            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);
            string yazi = sw.ReadLine();
            for (int k = 0; k < boyut; k++)
            {

                string[] degerler;
                degerler = yazi.Split(',');

                for (int i = 0; i < att_num; i++)
                {

                    hasta[k, i] = Convert.ToInt32(degerler[i]);
                    // MessageBox.Show(hasta[k,i].ToString());
                }
                yazi = sw.ReadLine();

            }

            sw.Close();
            fs.Close();



            for (int i = 0; i < boyut; i++)
            {
                for (int j = 0; j < att_num; j++)
                {
                    textBox1.Text = textBox1.Text + hasta[i, j].ToString() + " ";
                }
                textBox1.Text = textBox1.Text + Environment.NewLine;
            }




            karar_Entropi = Genel_Entropi(hasta);

            txt_ge.Text = karar_Entropi.ToString();



        }




        private void txt_ge_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cizim cizim = new cizim();
            cizim.Show();
            this.Hide();
        }
       
    }
}
