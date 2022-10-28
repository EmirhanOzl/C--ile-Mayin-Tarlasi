using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MayinTarlasi
{
    public partial class Form1 : Form
    {
        class Buton : Button         // class lı işlemlere tam alışamadım ilk başladığımda kodları dümdüz yazmıştım o yüzden yeniden class lada yapamadım bazı gerek şeyler olduğundan dolayı
        {                               // küçük bir kod yazdım
            public int sayim = 0;
            public bool bayrakVarmi = false;
            public bool mayindaBayrak = false;

        }

        Buton[,] buton = new Buton[14, 18];       // Oyundaki gereken bazı sayılar ve oyunun bitmesi ve kazanıldımı diye kontrol etmek için bool değişkenleri
        bool ilkClick = true;
        bool kazandiMi = true;
        bool oyunBitti = false;
        bool tek = true;
        int acilanButon = 0;
        int sayac = 0;
        int bayrakSayisi = 40;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Interval = 1000;  // Oyunda ne kadar süre geçirdiğini hesaplamak için timer


            var color = System.Drawing.Color.FromArgb(229, 194, 159);   // Buton renklerini kendim özelleştirdiğim için bazı yerlerde butonların rengi için değişken tanımmladım
            var color2 = System.Drawing.Color.FromArgb(215, 184, 153);

            int horizontal = 0;
            int vertical = 0;
            

            for (int i = 0; i < 14; i++)    // Bu for döngüsü ne butonların oluşması ve butonların özelleştirilmesi için kodlar yazılmıştır.
            {
                for (int j = 0; j < 18; j++)
                {
                    buton[i, j] = new Buton();

                    buton[i, j].Size = new Size(30, 30);
                    buton[i, j].FlatStyle = FlatStyle.Flat; 
                    buton[i, j].Name = (i +","+ j).ToString(); // ileriki bölümlerde tıklanan butonu bulmam için yazdım bunu daha kolay bi yöntem vardır ama bulamadım.
                    
                    if(i % 2 == 0) // yanyana olan butonların renklerinin aynı olmaması için yazılmış kodlar.
                    {
                        if (j % 2 == 0)
                        {
                            buton[i, j].BackColor = System.Drawing.Color.FromArgb(170, 215, 81);
                            buton[i, j].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 215, 81);
                        }
                        else
                        {
                            buton[i, j].BackColor = System.Drawing.Color.FromArgb(162, 209, 73);
                            buton[i, j].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(162, 209, 73);
                        }

                    }          // yanyana olan butonların renklerinin aynı olmaması için yazılmış kodlar.
                    else if(i % 2 == 1)
                    {
                        if (j % 2 == 1)
                        {
                            buton[i, j].BackColor = System.Drawing.Color.FromArgb(170, 215, 81);
                            buton[i, j].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 215, 81);
                        }
                        else
                        {
                            buton[i, j].BackColor = System.Drawing.Color.FromArgb(162, 209, 73);
                            buton[i, j].FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(162, 209, 73);
                        }
                    }
                    
                    buton[i, j].Location = new Point(horizontal, vertical);
                    buton[i, j].Click += new EventHandler(this.Buton_Click);  // butonlara tıklandığında yapılacak şeyler için yeni fonksiyon
                    buton[i, j].MouseDown += new MouseEventHandler(this.Buton_MouseDown);  // sağ tık için yazdığım yeni fonksiyon

                    panel2.Controls.Add(buton[i, j]);  // Butonları panele ekleme

                    if (j < 18)
                    {
                        horizontal += 30;
                        
                    }
                    if(j == 17)  // butonların 14 e 18 li bir matris olduğu için ve butonları alt satıra geçirmek için.
                    {
                        horizontal -= 540;
                        vertical += 30;
                    }
                }               
            }
        }

        void MayinlariKoy(Buton istisna)  // Mayınların bütün butonlar içinde rastgele oluşması için yazdığım fonksiyon.
        {
            Random rand = new Random();
            int mayinSayisi = 40;
            bool kontrol = true;
            int i, j;
            if (kontrol)
            {
                while (mayinSayisi > 0)
                {
                    i = rand.Next(14);
                    j = rand.Next(18);
                    if(buton[i, j] != istisna)
                    {
                        while (buton[i, j].Text == " ")
                        {
                            i = rand.Next(14);
                            j = rand.Next(18);
                        }

                        buton[i, j].Text = " ";  // butonda mayın olduğunu göstermek için text ini boşluk yaptım sonrasında oyun bittiğinde resmine mayın ekleniyor.
                    }
                    mayinSayisi--;
                }
            }
        }



        void AlaniAc(int i, int j)  // Basılan yerden itibaren mayınlara kadar olan yeri açması için yazdığım fonksiyon 
        {
            // Burada renkleri tanımlamamın sebebi sağ sol ön ve arkada olan yer tanımladığım renkte ise devam ediyor.
            // Bunu o butonlara tıklandığında devam etmesini istiyordum ancak yapamadım araştırdım bulamadım böylede oldu.

            var color = System.Drawing.Color.FromArgb(229, 194, 159);   // burada renkleri yine tanımlamam gerekti
            var color2 = System.Drawing.Color.FromArgb(215, 184, 153);
            if (i < 0 || j < 0 || i > 13 || j > 17 || buton[i, j].Text.Length > 0 || buton[i, j].BackColor == color || buton[i, j].BackColor == color2) // buton tanımladığım renkte ise ve 
                return;                                                                                                                                 // butonun sayısı 0 dan büyükse çalışmayıp yeniden girmesi için
            else if (buton[i,j].sayim != 0)      // butonun sayısı 0 değilse butonun textine sayısı yazmak ve renginii değiştirmek için
            {
                buton[i,j].Text = buton[i,j].sayim.ToString();
                buton[i, j].BackColor = color;
                acilanButon++;
            }
            else   // Mayınlara kadar yerlerin açılacağından dolayı rekürsif fonksiyon yaptım.
            {
                acilanButon++;
                buton[i, j].BackColor = color;
                AlaniAc(i + 1, j);
                AlaniAc(i - 1, j);
                AlaniAc(i, j + 1);
                AlaniAc(i, j - 1);
            }
        }
        
        void Buton_Click(object sender, EventArgs e)
        {
            timer1.Start();

            Buton btn = sender as Buton;  

            var color = System.Drawing.Color.FromArgb(229, 194, 159);
            var color2 = System.Drawing.Color.FromArgb(215, 184, 153);

            if (ilkClick)    // Mayınların yerlerini olşturduktan sonra birdaha buna girmemesi için 
            {

                for (int o = 0; o < 14; o++)
                {
                    for (int p = 0; p < 18; p++)
                    {
                        if (btn.Name == o + "," + p)
                        {
                            MayinlariKoy(btn);


                        }
                    }
                }
                ilkClick = false;

            }

            if(oyunBitti == false)    // oyun bittimi diye kontrol etmek için
            {

                if (tek == true)         // Mayınların etrafındaki butonların sayısını arttırmak için yazdım
                {
                    int etraftakiMayinSayisi = 0;
                    for (int i = 0; i < 14; i++)
                    {
                        for (int j = 0; j < 18; j++)
                        {
                            if (buton[i, j].Text == " ")
                            {

                                if (i == 0 && j == 0)
                                {
                                    buton[i + 1, j].sayim++;
                                    buton[i + 1, j + 1].sayim++;
                                    buton[i, j + 1].sayim++;
                                }             //Buradaki if else iflerin hepsi x = 0 yada 13 y = 0 yada 17  olduğunda program hata vermemesi için yazdım
                                else if (i == 0 && j != 0 && j != 17)
                                {
                                    buton[i + 1, j].sayim++;
                                    buton[i, j + 1].sayim++;
                                    buton[i + 1, j + 1].sayim++;
                                    buton[i + 1, j - 1].sayim++;
                                    buton[i, j - 1].sayim++;
                                }
                                else if (j == 0 && i != 0 && i != 13)
                                {
                                    buton[i - 1, j].sayim++;
                                    buton[i - 1, j + 1].sayim++;
                                    buton[i, j + 1].sayim++;
                                    buton[i + 1, j + 1].sayim++;
                                    buton[i + 1, j].sayim++;
                                }
                                else if (i == 13 && j == 17)
                                {
                                    buton[i - 1, j].sayim++;
                                    buton[i - 1, j - 1].sayim++;
                                    buton[i, j - 1].sayim++;
                                }
                                else if (i == 13 && j != 17 && j != 0)
                                {
                                    buton[i, j - 1].sayim++;
                                    buton[i - 1, j - 1].sayim++;
                                    buton[i - 1, j].sayim++;
                                    buton[i - 1, j + 1].sayim++;
                                    buton[i, j + 1].sayim++;
                                }
                                else if (j == 17 && i != 13 && i != 0)
                                {
                                    buton[i + 1, j].sayim++;
                                    buton[i + 1, j - 1].sayim++;
                                    buton[i, j - 1].sayim++;
                                    buton[i - 1, j - 1].sayim++;
                                    buton[i - 1, j].sayim++;
                                }
                                else if (j == 17 && i == 0)
                                {
                                    buton[i + 1, j].sayim++;
                                    buton[i + 1, j - 1].sayim++;
                                    buton[i, j - 1].sayim++;
                                }
                                else if (i == 13 && j == 0)
                                {
                                    buton[i, j + 1].sayim++;
                                    buton[i - 1, j + 1].sayim++;
                                    buton[i - 1, j].sayim++;
                                }
                                else
                                {
                                    for (int x = i - 1; x <= i + 1; x++)    // aslında asıl kod bu  mayının etrafındaki butonların sayısını arttırıyor
                                    {

                                        for (int y = j - 1; y <= j + 1; y++)
                                        {


                                            etraftakiMayinSayisi = buton[x, y].sayim;
                                            etraftakiMayinSayisi++;
                                            buton[x, y].sayim = etraftakiMayinSayisi;

                                        }
                                    }
                                }


                            }
                        }

                    }
                    tek = false;
                }

                for (int i = 0; i < 14; i++)          // üstte tanımladığım bastığımız alanın açılması için olan fonksiyonu buraya ekledim
                {
                    for (int j = 0; j < 18; j++)
                    {
                        if (btn.Name == i + "," + j)
                        {
                            AlaniAc(i, j);
                        }
                    }
                }

                


            }

            for (int i = 0; i < 14; i++)        // önceki kodlarda açılan butonları color ile tanımladığım renkle açtım ancak bu butonlar açılmadan önceki hali gibi yan yana olanların biri koyu olacak
            {                                           // o yüzden renklerinde oynama yaptım yeniden
                for (int j = 0; j < 18; j++)
                {
                    if (buton[i, j].BackColor == color)
                    {
                        if (i % 2 == 0)
                        {
                            if (j % 2 == 0)
                            {
                                buton[i, j].BackColor = color;
                                buton[i, j].FlatAppearance.BorderColor = color;
                            }
                            else
                            {
                                buton[i, j].BackColor = color2;
                                buton[i, j].FlatAppearance.BorderColor = color2;
                            }

                        }
                        else if (i % 2 == 1)
                        {
                            if (j % 2 == 1)
                            {
                                buton[i, j].BackColor = color;
                                buton[i, j].FlatAppearance.BorderColor = color;
                            }
                            else
                            {
                                buton[i, j].BackColor = color2;
                                buton[i, j].FlatAppearance.BorderColor = color2;
                            }
                        }
                    }
                }

            }

            acilanButon = 0;
            for (int i = 0; i < 14; i++)    // her tıkladığımda butonların hepsini tarayıp kaç tane color veya color2 renginde buton olduğunu bulmak için yazdım.
            {

                for (int j = 0; j < 18; j++)
                {
                    if (buton[i, j].BackColor == color || buton[i, j].BackColor == color2)
                    {
                        
                        acilanButon++;
                    }
                }

            }
            if (acilanButon == 212)       // color veya color2 rengindeki buton sayısı 212 olduğunda oyunu kazanmış oluyoruz. (Mayın 40 tane olduğundan dolayı)
            {
               kazandiMi = true;
               oyunBitti = true;
            }

            if (oyunBitti == true)         // Eğer oyun bittiyse ve vayrak koyduğumuz butonda mayın varsa o butona tik işareti koyuyor.
            {                               // ama bayrak koyulduğu halde butonda mayın yoksa çarpı işareti koyuyor.
                for (int i = 0; i < 14; i++)
                {

                    for (int j = 0; j < 18; j++)
                    {
                        if (buton[i, j].Text == " ")
                        {
                            if(buton[i, j].mayindaBayrak == true)
                                buton[i, j].Image = Properties.Resources.tik;
                        }
                        if (buton[i, j].Text != " " && buton[i, j].bayrakVarmi == true)
                        {
                            buton[i, j].Image = Properties.Resources.close;
                        }
                    }

                }
            }

            if (oyunBitti == true)    // Oyun bittiyse süreyi durduruyor ve kazandıysa kazandınız yazısı mayına bastıysa mayına bastınız yazısı geliyor.
            {
                if (kazandiMi == true)
                {
                    timer1.Stop();
                    MessageBox.Show("Tebrikler Kazandiniz \nSüre : " + sayac + " saniye");
                }
                else
                {
                    timer1.Stop();
                    MessageBox.Show("Mayına Bastınız.\nSüre : " + sayac + " saniye");
                }
            }
        }

        void Buton_MouseDown(object sender, MouseEventArgs e)   // Buna daha çok sağ click için ihtiyaç duydum o yüzden yazdım
        {
            Buton btn1 = (Buton)sender;
            var mayin = Properties.Resources.mine;  // mayın resmini bir değişkene atadım
            
            if (e.Button == MouseButtons.Left) 
            {
                if (btn1.Text == " ")  // Eğer basılan butonda mayın varsa diğer bütün mayınlı butonları açığa çıkarmak için yazdım.
                {
                    for (int o = 0; o < 14; o++)
                    {
                        for (int p = 0; p < 18; p++)
                        {
                            if (buton[o, p].Text == " ")
                            {
                                buton[o, p].Image = mayin;
                                kazandiMi = false;
                                oyunBitti = true;
                            }
                        }

                    }

                }  
            }
            else if (e.Button == MouseButtons.Right)
            {
                if(oyunBitti == false)        // bayrak işlemleri bu bölümde yazılı ( eğer oyun bitmediyse )
                {
                    if (btn1.bayrakVarmi == false)   // butonda bayrak yoksa butona bayrak koyuyor.
                    {
                        if (btn1.Text == " ")    // eğer bayrak koyduğumuz buton mayınlıysa mayın resmi kalktığından dolayı  mayindaBayrak adlı değişkeni true yapıyorum.
                        {
                            btn1.Image = Properties.Resources.redflag;
                            btn1.bayrakVarmi = true;
                            bayrakSayisi--;
                            label1.Text = bayrakSayisi.ToString();
                            btn1.mayindaBayrak = true;  // butonda bayrak varsa aşağıki kodlarda lazım olacağından dolayı yazdım.
                        }
                        else   // üstteki kodlarla tamamen butonda mayın yoksa buna giriyor ve mayindaBayrak değişkeni değişmiyor.
                        {
                            btn1.Image = Properties.Resources.redflag;
                            btn1.bayrakVarmi = true;
                            bayrakSayisi--;
                            label1.Text = bayrakSayisi.ToString();
                        }
                    }
                    else       // eğer butonda bayrak varsa butondaki bayrağı kaldırmak için yazdım.
                    {
                        if (btn1.mayindaBayrak == true)  // bayrağı kaldıracağımız butonda önceden mayın varsa kaldırdığımızda yenidne mayın ekleniyor.
                        {
                            if(oyunBitti == true)
                                btn1.Image = Properties.Resources.mine;
                            btn1.mayindaBayrak = false;
                            btn1.bayrakVarmi = false;
                            bayrakSayisi++;
                            label1.Text = bayrakSayisi.ToString();
                        }
                        else   // kodlar aynı mayın yoksa direk bayrağı alıyor.
                        {
                            btn1.Image = null;
                            btn1.bayrakVarmi = false;
                            bayrakSayisi++;
                            label1.Text = bayrakSayisi.ToString();
                        }
                    }
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            sayac++;
            label2.Text = sayac.ToString();
        }
    }
}
