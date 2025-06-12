using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KitapVerUyeSecFormu : Form
    {
        public string Selected_KitapAdi { get; set; }
        public string Selected_KitapYazari { get; set; }
        public string Selected_KitapBasimEvi { get; set; }
        public string Selected_KitapBasimYili { get; set; }
        public int Selected_KacTane { get; set; } 

        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KitapVerUyeSecFormu()
        {
            InitializeComponent();
            
        }

        private void KitapVerUyeSecFormu_Load(object sender, EventArgs e)
        {
           

            Control lblAdi = this.Controls.Find("lblSecilenKitapAdi", true).FirstOrDefault();
            if (lblAdi is Label labelAdi) labelAdi.Text = this.Selected_KitapAdi ?? "N/A";

            Control lblYazar = this.Controls.Find("lblSecilenKitapYazari", true).FirstOrDefault();
            if (lblYazar is Label labelYazar) labelYazar.Text = this.Selected_KitapYazari ?? "N/A";

            Control lblBasimEvi = this.Controls.Find("lblSecilenKitapBasimEvi", true).FirstOrDefault();
            if (lblBasimEvi is Label labelBasimEvi) labelBasimEvi.Text = this.Selected_KitapBasimEvi ?? "N/A";

            Control lblBasimYili = this.Controls.Find("lblSecilenKitapBasimYili", true).FirstOrDefault();
            if (lblBasimYili is Label labelBasimYili) labelBasimYili.Text = this.Selected_KitapBasimYili ?? "N/A";

            Control lblStok = this.Controls.Find("lblSecilenKitapStok", true).FirstOrDefault();
            if (lblStok is Label labelStok) labelStok.Text = this.Selected_KacTane.ToString();


            LoadUyelerToComboBox();
        }

        private void LoadUyelerToComboBox()
        {
            try
            {
                DataTable dtUyeler = vi.UyelerComboBoxListesi();

                if (dtUyeler != null && dtUyeler.Rows.Count > 0)
                {
                    uyeEposta.DataSource = dtUyeler;
                    uyeEposta.DisplayMember = "Eposta"; 
                    uyeEposta.ValueMember = "Uye_No";  
                    uyeEposta.SelectedIndex = 0; 
                    UpdateUyeBilgileriTextBox();
                }
                else
                {
                  
                    TextBox txtUyeAdi = this.Controls.Find("uyeAdi", true).FirstOrDefault() as TextBox;
                    TextBox txtUyeSoyadi = this.Controls.Find("uyeSoyadi", true).FirstOrDefault() as TextBox;

                    if (txtUyeAdi != null) txtUyeAdi.Text = string.Empty;
                    if (txtUyeSoyadi != null) txtUyeSoyadi.Text = string.Empty;

                    btnKitabiVer.Enabled = false; // Kitap ver butonu pasif
                    MessageBox.Show("Sistemde kayıtlı üye bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Üyeler yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close(); 
            }
        }

        private void uyeEposta_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUyeBilgileriTextBox();
        }

        private void UpdateUyeBilgileriTextBox()
        {
            TextBox txtUyeAdi = this.Controls.Find("uyeAdi", true).FirstOrDefault() as TextBox;
            TextBox txtUyeSoyadi = this.Controls.Find("uyeSoyadi", true).FirstOrDefault() as TextBox;

            if (uyeEposta.SelectedItem != null && uyeEposta.SelectedItem is DataRowView seciliUyeRowView)
            {
                if (txtUyeAdi != null) txtUyeAdi.Text = seciliUyeRowView["UyeAdi"].ToString();
                if (txtUyeSoyadi != null) txtUyeSoyadi.Text = seciliUyeRowView["UyeSoyadi"].ToString();
            }
            else
            {
                if (txtUyeAdi != null) txtUyeAdi.Text = string.Empty;
                if (txtUyeSoyadi != null) txtUyeSoyadi.Text = string.Empty;
            }
        }

        private void btnKitabiVer_Click(object sender, EventArgs e)
        {
            if (uyeEposta.SelectedIndex == -1 || uyeEposta.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir üye seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TextBox txtUyeAdi = this.Controls.Find("uyeAdi", true).FirstOrDefault() as TextBox;
            TextBox txtUyeSoyadi = this.Controls.Find("uyeSoyadi", true).FirstOrDefault() as TextBox;
            string mesajUyeAdi = txtUyeAdi?.Text ?? "";
            string mesajUyeSoyadi = txtUyeSoyadi?.Text ?? "";
            string mesajUyeEposta = uyeEposta.Text;

            DialogResult onaySonucu = MessageBox.Show(
                $"Üye Adı: {mesajUyeAdi}\n" +
                $"Üye Soyadı: {mesajUyeSoyadi}\n" +
                $"Eposta: {mesajUyeEposta}\n" +
                $"olan üyeye\n\n" +
                $"Kitap Adı: {this.Selected_KitapAdi}\n" +
                $"Yazarı: {this.Selected_KitapYazari}\n" +
                $"Basım Evi: {this.Selected_KitapBasimEvi}\n" +
                $"Basım Yılı: {this.Selected_KitapBasimYili}\n" +
                "kitabını ödünç vermek istiyor musunuz?",
                "KİTAP VER ONAY",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (onaySonucu == DialogResult.Yes)
            {
                // try-catch bloğunu burada tutmaya gerek yok,
                // VeritabaniIslemleri içindeki metot hataları yakalayıp logluyor ve dönüş değeri veriyor.
                // Ancak isterseniz genel bir UI hatası için tutabilirsiniz.

                if (this.Selected_KacTane <= 0)
                {
                    MessageBox.Show("Bu kitaptan stokta kalmamış, ödünç verilemez.", "Stok Yok", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int kitapId = vi.KitapIdGetir(this.Selected_KitapAdi, this.Selected_KitapYazari, this.Selected_KitapBasimEvi, this.Selected_KitapBasimYili);

                if (kitapId == 0)
                {
                    MessageBox.Show("Kitap sistemde bulunamadı veya bilgiler eşleşmiyor. Lütfen kitap bilgilerini kontrol edin.", "Kitap Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string alisTarihi = DateTime.Now.ToString("dd.MM.yyyy");
                int secilenUyeNo = Convert.ToInt32(uyeEposta.SelectedValue);

                int sonucKodu = vi.KitapOduncVerTransactionYok(kitapId, secilenUyeNo, alisTarihi, this.Selected_KitapAdi, this.Selected_KitapYazari, this.Selected_KitapBasimEvi, this.Selected_KitapBasimYili);

                switch (sonucKodu)
                {
                    case 0: // Başarılı
                        MessageBox.Show("Kitap başarıyla verildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case 1: // AlinanKitaplar'a ekleme hatası (Kayıt çakışması)
                        MessageBox.Show("Bu kitap bu üyeye bu tarihte zaten ödünç verilmiş olabilir veya başka bir benzersizlik kuralı ihlal edildi.\nLütfen kayıtları kontrol edin.", "Kayıt Çakışması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2: // Stok güncelleme hatası
                        MessageBox.Show("Kitap stoku işlem sırasında tükendi, zaten sıfırdı veya belirtilen kriterlere uyan kitap bulunamadı.\nİlk ödünç verme işlemi başarılı oldu ancak stok güncellenemedi. Lütfen sistem yöneticisine başvurun!", "Stok/Güncelleme Tutarsızlığı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Bu durumda ilk formdaki listeyi yenilemek yanıltıcı olabilir,
                        // çünkü stok azalmamış görünebilir.
                        this.DialogResult = DialogResult.Abort; // Veya farklı bir DialogResult
                        this.Close();
                        break;
                    case 3: // Diğer genel veya SQL hatası
                        MessageBox.Show("Kitap verme işlemi sırasında bir veritabanı veya genel hata oluştu. Lütfen sistem yöneticisine başvurun.", "İşlem Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("Bilinmeyen bir hata durumu oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e) // Eğer butonun adı btnIptal ise
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}