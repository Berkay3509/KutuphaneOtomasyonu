using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kutuphane
{
    public partial class GirisFormu : Form
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            string[] sonuc = vi.kullaniciGirisKontrolu(kullaniciAdiTxtBx.Text, sifreTxtBx.Text);

            if (sonuc[0] == "1")
            {
                string kullaniciAdi = sonuc[1];
                int yetki = Convert.ToInt32(sonuc[2]);
                int uye_no = Convert.ToInt32(sonuc[3]);
                if (yetki == 1)
                {
                    //Yonetici Formu
                    YoneticiFormu yFrm = new YoneticiFormu();
                    this.Hide();         //Giriş formunun saklanması için
                    kullaniciAdiTxtBx.Text = ""; //Textbox'ların içini temizliyoruz
                    sifreTxtBx.Text = "";      //Textbox'ların içini temizliyoruz
                    yFrm.ShowDialog(); //Dialog penceresi olarak açılmasını sağladığımızdan alttaki this.Show(); kodu bu form kapatılana dek çalışmayacaktır.
                    this.Show();       //Giriş formunu tekrar göstermek için
                    kullaniciAdiTxtBx.Focus();
                }
                else
                {
                    //Kullanıcı Formu
                    KullaniciFormu kFrm = new KullaniciFormu();
                    this.Hide();         //Giriş formunun saklanması için
                    kullaniciAdiTxtBx.Text = ""; //Textbox'ların içini temizliyoruz
                    sifreTxtBx.Text = "";      //Textbox'ların içini temizliyoruz
                    kFrm.kullaniciAdi = kullaniciAdi;
                    kFrm.uye_no = uye_no;
                    kFrm.ShowDialog(); //Kullanıcı formunu burada ShowDialog() ile göstereceğiz
                    this.Show();       //Giriş formunu tekrar göstermek için
                    kullaniciAdiTxtBx.Focus();
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre hatalı", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GirisFormu_Load(object sender, EventArgs e)
        {

        }
    }
}
