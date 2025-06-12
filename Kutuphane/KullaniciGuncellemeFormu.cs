using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KullaniciGuncellemeFormu : Form
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri(); 
        public KullaniciGuncellemeFormu()
        {
            InitializeComponent();
            KullaniciAdiDoldur(); 
            comboBox1.SelectedIndex = -1; 
            TemizleFormAlanlarini();
        }
        void KullaniciAdiDoldur()
        {
            try
            {
                DataTable dt = vi.SistemKullanicilariComboBoxListesi();
                if (dt != null)
                {
                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "KullaniciAdi";
                    comboBox1.ValueMember = "KullaniciAdi"; 
                }
                else
                {
                    MessageBox.Show("Kullanıcı listesi yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Kullanıcılar yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedItem is DataRowView selectedRowView)
            {
                try
                {
                    textBoxKullaniciAd2.Text = selectedRowView["KullaniciAdi"].ToString();
                    textBoxSifre.Text = selectedRowView["Sifre"].ToString();

                    if (selectedRowView["Yonetici"] != DBNull.Value)
                    {
                        string yoneticiDurumu = selectedRowView["Yonetici"].ToString().Trim();
                        yoneticiChkbx.Checked = (yoneticiDurumu == "1");
                    }
                    else
                    {
                        yoneticiChkbx.Checked = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kullanıcı bilgileri alanlara yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TemizleFormAlanlarini();
                }
            }
            else
            {
                TemizleFormAlanlarini(); 
            }
        }

        private void TemizleFormAlanlarini()
        {
            textBoxKullaniciAd2.Text = "";
            textBoxSifre.Text = "";
            yoneticiChkbx.Checked = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen güncellemek için bir kullanıcı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxKullaniciAd2.Text))
            {
                MessageBox.Show("Yeni kullanıcı adı boş olamaz!", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxKullaniciAd2.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(textBoxSifre.Text))
            {
                MessageBox.Show("Şifre boş olamaz!", "Geçersiz Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxSifre.Focus();
                return;
            }

            try
            {
                DataRowView selectedRowView = (DataRowView)comboBox1.SelectedItem;
                string eskiKullaniciAdi = selectedRowView["KullaniciAdi"].ToString(); 

                string yeniKullaniciAdi = textBoxKullaniciAd2.Text.Trim();
                string sifre = textBoxSifre.Text;
                string yonetici = yoneticiChkbx.Checked ? "1" : "0";

                if (vi.SistemKullanicisiGuncelle(eskiKullaniciAdi, yeniKullaniciAdi, sifre, yonetici))
                {
                    MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
                    string guncellenenKullaniciAdi = yeniKullaniciAdi; 
                    KullaniciAdiDoldur();
                    SelectUserInComboBox(guncellenenKullaniciAdi);
                }
                else
                {
                    MessageBox.Show("Güncelleme yapılamadı. Kullanıcı bulunamadı, veritabanı hatası oluştu veya hiçbir değişiklik yapılmadı.", "Güncelleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Güncelleme sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectUserInComboBox(string kullaniciAdi)
        {
            
            for (int i = 0; i < comboBox1.Items.Count; i++)
            {
                if (comboBox1.Items[i] is DataRowView drv)
                {
                    if (drv["KullaniciAdi"].ToString().Equals(kullaniciAdi, StringComparison.OrdinalIgnoreCase))
                    {
                        comboBox1.SelectedIndex = i;
                        return;
                    }
                }
            }
            
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            else comboBox1.SelectedIndex = -1;
        }

        private void btn_Iptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void KullaniciGuncellemeFormu_Load(object sender, EventArgs e)
        {
            
        }
    }
}