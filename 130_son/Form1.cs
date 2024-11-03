using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _130_son
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //program açılırken
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] meslekler =
            {
                "Mühendis","Öğretmen","Öğrenci","İşçi",
                "Programcı","Muhasebeci","Satış","Pazarlama",
                "Ekonomist","Müfettiş","Avukat","Mimar"
            };
            comboBox_meslek.Items.AddRange(meslekler);

            string[] birimler =
          {
                "Satış ","Eğitim","Pazarlama","Reklam",
                "Üretim","Muhasebe","Teknik Destek","Yazılım",
                "NEtwork","Yönetim","Çağrı Merkezi"
            };
            comboBox_birim.Items.AddRange(birimler);

            try
            {
                //dosya ac
                System.IO.TextReader dosya_ac = System.IO.File.OpenText("C:\\Users\\nurii\\OneDrive\\Desktop\\unsinDosya2");
                string satir;
                
                //dosyadaki bilgileri listbox lara attık
                while ((satir=dosya_ac.ReadLine())!=null)
                {
                    listBox1.Items.Add(satir);

                    satir=dosya_ac.ReadLine();
                    listBox2.Items.Add(satir);

                    satir = dosya_ac.ReadLine();
                    listBox3.Items.Add(satir);

                }
                dosya_ac.Close();
            }
            catch 
            {
                //Program ilk çalıştırıldığında dosya da hiç veri olmayacağı için cathch blokları çalışacak
                //Ancak burda herhangi bir işlem yapılmayacak.
                
            }
            label5.Text = listBox1.Items.Count.ToString();
            label7.Text=(listBox1.SelectedIndex+1).ToString();
        }

        //program kapatılırken
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //dosya oluşturulur
            System.IO.TextWriter dosya_kaydet = System.IO.File.CreateText("C:\\Users\\nurii\\OneDrive\\Desktop\\unsinDosya2");
            
            //listelerdeeki bilgiler for döngüsü yardımı ile dosyaya atılıır.
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                dosya_kaydet.WriteLine(listBox1.Items[i]);
                dosya_kaydet.WriteLine(listBox2.Items[i]);  
                dosya_kaydet.WriteLine(listBox3.Items[i]);

            }
            dosya_kaydet.Close();

        }

        //listboxlardan herhangi bir öge seçildiğinde ilişkili(aynı indis) ögelerde seçilmesini sağlayan metod
        //bu metodu tüm listBoxların SelectIndexChanced propertiesine ekeledim
        private void listbox_secim (object sender, System.EventArgs e)
        {
            int indis;
            indis=(sender as ListBox).SelectedIndex;

            listBox1.SelectedIndex= indis;
            listBox2.SelectedIndex=indis;
            listBox3.SelectedIndex=indis;
            //listbox kontrolleri çok uzunsa kaydırma çubukları gelecektir
            //bu durumda listeler aynı hizada olmayabilr
            //bunun için aynı hizaya getirilir
            int top_indis;
            top_indis = (sender as ListBox).TopIndex;
            listBox1.TopIndex= top_indis;
            listBox2.TopIndex= top_indis;
            listBox3.TopIndex= top_indis;
            //listelerde seçili olan ögelerim textbox ve combobox üzerinde gösterilir
            textBox_adsyoad.Text=listBox1.Text;
            comboBox_meslek.Text=listBox2.Text;
            comboBox_birim.Text=listBox3.Text;

            label7.Text=(listBox1.SelectedIndex+1).ToString();

        }

        private void button_ekle_Click(object sender, EventArgs e)
        {
            int yer;
            yer = listBox1.FindStringExact(textBox_adsyoad.Text); //yazılan ad soyadı listede ara
            if(yer<0) //yoksa
            {
                listBox1.Items.Add(textBox_adsyoad.Text);
                listBox2.Items.Add(comboBox_meslek);
                listBox3.Items.Add(comboBox_birim); 
                
                label5.Text=listBox1.Items.Count.ToString();

            }
            else
            {
                listBox1.SelectedIndex = yer;//bulunan kayıt
                MessageBox.Show("bu eleman listede zaten var");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex<0)
            {
                MessageBox.Show("önce silecenek elemanı seçiniz");
                return;
            }

            int indis=listBox1.SelectedIndex;
            DialogResult c;
            c=MessageBox.Show(listBox1.SelectedItem+"silinsin mi?","silme",MessageBoxButtons.YesNo);
            if(c==DialogResult.Yes)
            {
                listBox1.Items.RemoveAt(indis);
                listBox2.Items.RemoveAt(indis);
                listBox3.Items.RemoveAt(indis); 

                label5.Text=listBox1.Items.Count.ToString();  
                
            }



        }
        //arama işleminin sürekli olarak en son kalan yerden devam edebilmesi için 
        //aranan kelimenin bulunduğu yer bilgileri tutuluyor.
        //onun için yer bilgisi global tanımlanıyor
        int yer = 1;
        string aranan;

        private void button4_Click(object sender, EventArgs e)
        {
            aranan=textBox_aranan.Text;
            yer = listBox1.FindStringExact(aranan);

            if (yer < 0)
            {
                //bulunamazsa bir benzerini ara
                yer = listBox1.FindString(aranan);

                if (yer < 0)
                    MessageBox.Show("aranan değer bulunamadı");

            }
            else //bulunursa
            {
                listBox1.SelectedIndex = yer;
            }



              

            


        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (yer < listBox1.Items.Count - 1)
            {
                yer = listBox1.FindString(aranan, yer);

                if(yer>=0)
                {
                    listBox1.SelectedIndex=yer;
                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int indis;
            string adsoyad, meslek, birim;

            adsoyad=textBox_adsyoad.Text;
            meslek=comboBox_meslek.Text;
            birim=comboBox_birim.Text;

            indis = listBox1.SelectedIndex;
            if (indis < 0)
            {
                MessageBox.Show("önce değiştirilecek kişiyi seçin");
            }
            else
            {
                listBox1.Items[indis] = adsoyad;
                listBox2.Items[indis]=meslek;
                listBox3.Items[indis] = birim;
            }
        }

        private void button_arayaekle_Click(object sender, EventArgs e)
        {
            int indis;
            indis=listBox1.SelectedIndex;

            if (indis < 0)
            {
                MessageBox.Show("önce elemanın nereye ekeleneceğini seçiniz:");
            }
            else
            {
                int yer;
                yer=listBox1.FindStringExact(textBox_adsyoad.Text);

                if (yer < 0)
                {
                    listBox1.Items.Insert(indis, textBox_adsyoad);
                    listBox2.Items.Insert(indis,comboBox_meslek);
                    listBox3.Items.Insert(indis,comboBox_birim);

                    label5.Text=listBox1.Items.Count.ToString();

                }
                else
                {
                    listBox1.SelectedIndex = yer;
                    MessageBox.Show("bu eleman zaten var");
                }

            }
        }

        private void button_yenikisi_Click(object sender, EventArgs e)
        {
            textBox_adsyoad.Text = "";
            comboBox_meslek.Text = "";
            comboBox_birim.Text = "";
        }
    }
}
