using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kutuphane
{
    public partial class UyeEklemeFormu : Form
    {
        public UyeEklemeFormu()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string uye_adi = adTxtBx.Text;
            string uye_soyadi = soyadiTxtBx.Text;
            string tel = telTxtBx.Text;
            string adres = adresRichTxtBx.Text;
            string eposta = epostaTxtBx.Text;
            string dogumTarihi = dogumTarihiDateTimePicker.Value.ToShortDateString(); 
            string uyelikTarihi = uyelikTarihiDateTimePicker.Value.ToShortDateString();
            string kullaniciAdi = kullaniciAdiTxtBx.Text;
            string sifre = sifreTxtBx.Text;
            string yonetici = "0";
            if (yoneticiChkbx.Checked == true)
            {
                yonetici = "1";
            }

            
            if (string.IsNullOrWhiteSpace(adTxtBx.Text))
            {
                MessageBox.Show("Lütfen üye adını giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                adTxtBx.Focus(); 
                return; 
            }

            if (string.IsNullOrWhiteSpace(soyadiTxtBx.Text))
            {
                MessageBox.Show("Lütfen üye soyadını giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                soyadiTxtBx.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(telTxtBx.Text))
            {
                MessageBox.Show("Lütfen telefon numarasını giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                telTxtBx.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(adresRichTxtBx.Text))
            {
                MessageBox.Show("Lütfen adresi giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                adresRichTxtBx.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(epostaTxtBx.Text))
            {
                MessageBox.Show("Lütfen e-posta adresini giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                epostaTxtBx.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(kullaniciAdiTxtBx.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adını giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kullaniciAdiTxtBx.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(sifreTxtBx.Text))
            {
                MessageBox.Show("Lütfen şifreyi giriniz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                sifreTxtBx.Focus();
                return;
            }
            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            if (vi.YoneticiUyeEkleme(uye_adi, uye_soyadi, tel, adres, eposta, uyelikTarihi, dogumTarihi, kullaniciAdi, sifre, yonetici))
            {
                MessageBox.Show("Kullanıcı eklendi");
            }
            else
            {
                MessageBox.Show("Kullanıcı eklenemedi", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UyeEklemeFormu_Load(object sender, EventArgs e)
        {

        }

        private void yoneticiChkbx_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void adTxtBx_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
