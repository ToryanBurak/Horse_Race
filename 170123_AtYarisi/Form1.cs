using _170123_AtYarisi.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _170123_AtYarisi
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }
        //Global Alan
        Horse_Select? slc = new Horse_Select(); //Hangi Atın Seçildiği enumu
        Winner_Horse? winner = new Winner_Horse(); //Hangi Atın Yarışı Kazandığı enumu
        int h1, h2, h3, h4; //Atların Hızları
        int hpwdsayac = 0; //Atlara ilk hızı verebilmek için gerekli
        Random rnd = new Random(); //Random Sayı komutu için gereken statik sınıf

        //At İsimleri 
        private void Form_Load(object sender, EventArgs e)
        {
            string[] check = new string[] { "1.At", "2.At", "3.At", "4.At" };
            this.Icon = Properties.Resources.Horse_İco;
            cmb_Horse_Select.Items.Add(check[0]);
            cmb_Horse_Select.Items.Add(check[1]);
            cmb_Horse_Select.Items.Add(check[2]);
            cmb_Horse_Select.Items.Add(check[3]);
            btnStop.Enabled = false;

        }
        
        //Başla Butonuna Basıldığında yapılacak Şeyler
        private void btnStart_Click(object sender, EventArgs e)
        {
            //At Giflerinin Hareketinin Başlaması
            horse1.Enabled = true;
            horse2.Enabled = true;
            horse3.Enabled = true;
            horse4.Enabled = true;
            //Bahsin Girilme Onayı
            switch (cmb_Horse_Select.Text)
            {
                case "1.At": slc = Horse_Select.Horse1; break;
                case "2.At": slc = Horse_Select.Horse2; break;
                case "3.At": slc = Horse_Select.Horse3; break;
                case "4.At": slc = Horse_Select.Horse4; break;
                case "": slc = Horse_Select.NoSelect; break;
            }
            //Yarışın Başlaması
            if (cmb_Horse_Select.Text != string.Empty) //Bahis Girilmeden Yarış Başlamaz
            {
                race_Timer.Start();
                Horse_Power_Timer.Start();
                btnStart.Enabled = false;
                cmb_Horse_Select.Enabled = false;
                btnStop.Enabled = true;
            }
            else 
            { 
                MessageBox.Show("Lütfen Bahis Giriniz"); 
                sifirla();
            }

            
        }
        
        //Yarış Timerı
        private void race_Timer_Tick(object sender, EventArgs e)
        {
            //Yarış Başladıktan 3.Saniyeye kadar değişkenler 0 kaldığı için atlar hareket etmemekte.Onun için ilk hızlarını sayac oluşturarak belirledik
            if (hpwdsayac == 0)
            {
                h1 = rnd.Next(2, 5);
                h2 = rnd.Next(2, 5);
                h3 = rnd.Next(2, 5);
                h4 = rnd.Next(2, 5);
            }
            hpwdsayac++;
            //Atların Hareketi
             horse1.Location = new Point(horse1.Location.X + h1, horse1.Location.Y);
             horse2.Location = new Point(horse2.Location.X + h2, horse2.Location.Y);
             horse3.Location = new Point(horse3.Location.X + h3, horse3.Location.Y);
             horse4.Location = new Point(horse4.Location.X + h4, horse4.Location.Y);
            
            
            //Yarışın Bitmesi
            

            if (horse1.Location.X >= btnEnd.Location.X-95) { race_Timer.Stop(); }
            if (horse2.Location.X >= btnEnd.Location.X-95) { race_Timer.Stop(); }
            if (horse3.Location.X >= btnEnd.Location.X-95) { race_Timer.Stop(); }
            if (horse4.Location.X >= btnEnd.Location.X-95) { race_Timer.Stop(); }
            if (race_Timer.Enabled == false)
            {
                //Bitiş Çizgisi Konumuna Göre Kazanan Atın Belirlenmesi
                int[] Location = { Convert.ToInt32(horse1.Location.X), Convert.ToInt32(horse2.Location.X), Convert.ToInt32(horse3.Location.X), Convert.ToInt32(horse4.Location.X) };
                Array.Sort(Location);
                if (Location[3] == Convert.ToInt32(horse1.Location.X)) { MessageBox.Show("Kazanan 1.At"); winner = Winner_Horse.Horse1; }
                if (Location[3] == Convert.ToInt32(horse2.Location.X)) { MessageBox.Show("Kazanan 2.At"); winner = Winner_Horse.Horse2; }
                if (Location[3] == Convert.ToInt32(horse3.Location.X)) { MessageBox.Show("Kazanan 3.At"); winner = Winner_Horse.Horse3; }
                if (Location[3] == Convert.ToInt32(horse4.Location.X)) { MessageBox.Show("Kazanan 4.At"); winner = Winner_Horse.Horse4; }
                //Bahsin Sorgusu
                if (winner.ToString() == slc.ToString()) { MessageBox.Show("Tebrikler Kazandınız!!!"); }
                else MessageBox.Show("Bir Daha Ki Sefere");
                //Yarışın Bitmesi
                sifirla();

            }
            

        }
        
        //Atların Hızlarının 3 saniyede bir Değişmesi
        private void Horse_Power_Timer_Tick(object sender, EventArgs e)
        {
            
            h1 = rnd.Next(2, 5);
            h2 = rnd.Next(2, 5);
            h3 = rnd.Next(2, 5);
            h4 = rnd.Next(2, 5);
            hpwdsayac++;
        }
        //Yarışı Bitir Butonu
        private void btnStop_Click(object sender, EventArgs e)
        {
            sifirla();
            
        }
        public void sifirla()
        {
            cmb_Horse_Select.Enabled = true;
            btnStart.Enabled = true;
            horse1.Location = new Point(12,horse1.Location.Y);
            horse2.Location = new Point(12,horse2.Location.Y);
            horse3.Location = new Point(12,horse3.Location.Y);
            horse4.Location = new Point(12,horse4.Location.Y);
            Horse_Power_Timer.Stop();
            race_Timer.Stop();
            slc = null;
            winner = null;
            horse1.Enabled = false;
            horse2.Enabled = false;
            horse3.Enabled = false;
            horse4.Enabled = false;
            btnStop.Enabled = false;
            hpwdsayac = 0;

        }
        
    }
}

