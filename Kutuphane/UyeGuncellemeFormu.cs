using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class UyeGuncellemeFormu : Form
    {
        public UyeGuncellemeFormu()
        {
            InitializeComponent();

            ComboboxDoldur();
            comboBox1.SelectedIndex = -1;

        }

        VeritabaniIslemleri vi = new VeritabaniIslemleri();


        private void UyeGuncellemeFormu_Load(object sender, EventArgs e)
        {

            TemizleFormAlanlarini();
        }

        void ComboboxDoldur()
        {
            comboBox1.DataSource = new BindingSource(vi.YoneticiComboboxDataTable(), null);
            comboBox1.DisplayMember = "Üye E-Posta Adresi";
            comboBox1.ValueMember = "Uye_No";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                string[] sonuc = vi.YoneticiUyeNoyaGoreUyeBilgisi(comboBox1.SelectedValue.ToString());
                if (sonuc[1] != "-1")
                {
                    try
                    {
                        adTxtBx.Text = sonuc[1];
                        soyadiTxtBx.Text = sonuc[2];
                        epostaTxtBx.Text = sonuc[3];
                        telTxtBx.Text = sonuc[4];
                        dogumTarihiDateTimePicker.Value = DateTime.Parse(sonuc[5]); 
                        uyelikTarihiDateTimePicker.Value = DateTime.Parse(sonuc[6]); 
                        adresRichTxtBx.Text = sonuc[7];
                    }
                    catch
                    { }
                }
                else
                {
                    MessageBox.Show("Veritabanı bağlantı hatası oluştu.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TemizleFormAlanlarini()
        {
            adTxtBx.Text = "";
            soyadiTxtBx.Text = "";
            epostaTxtBx.Text = "";
            telTxtBx.Text = "";
            adresRichTxtBx.Text = "";
            dogumTarihiDateTimePicker.Value = DateTime.Now;
            dogumTarihiDateTimePicker.Checked = false;
            uyelikTarihiDateTimePicker.Value = DateTime.Now;
            uyelikTarihiDateTimePicker.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Lütfen güncellemek için bir üye seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(adTxtBx.Text) || string.IsNullOrWhiteSpace(soyadiTxtBx.Text) || string.IsNullOrWhiteSpace(epostaTxtBx.Text))
            {
                MessageBox.Show("Ad, Soyad ve E-posta alanları boş bırakılamaz.", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string uye_adi = adTxtBx.Text;
            string uye_soyadi = soyadiTxtBx.Text;
            string tel = telTxtBx.Text;
            string adres = adresRichTxtBx.Text;
            string eposta = epostaTxtBx.Text;
            string dogumTarihi = dogumTarihiDateTimePicker.Value.ToShortDateString();
            string uyelikTarihi = uyelikTarihiDateTimePicker.Value.ToShortDateString();

            if (vi.YoneticiUyeGuncelleme(comboBox1.SelectedValue.ToString(), uye_adi, uye_soyadi, tel, adres, eposta, uyelikTarihi, dogumTarihi))
            {
                MessageBox.Show("Üye bilgileri başarılı bir şekilde güncellendi", "Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ComboboxDoldur(); 
            }
            else
            {
                MessageBox.Show("Veritabanı bağlantı hatası oluştu.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}