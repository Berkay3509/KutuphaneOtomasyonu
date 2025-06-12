using System;
using System.Windows.Forms;
namespace Kutuphane
{
    public partial class KitapEklemeFormu : Form
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();
        public KitapEklemeFormu()
        {
            InitializeComponent();
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            string kitapAdi = kitapAdTxtBx.Text.Trim();
            string yazar = yazarTxtBx.Text.Trim();
            string basimEvi = eviTxtBx.Text.Trim();
            string basimYili = yiliTxtBx.Text.Trim();
            int adet = (int)numericUpDownAdet.Value;
            if (string.IsNullOrWhiteSpace(kitapAdi))
            {
                MessageBox.Show("Kitap adı boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                kitapAdTxtBx.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(yazar))
            {
                MessageBox.Show("Yazar adı boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                yazarTxtBx.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(basimEvi))
            {
                MessageBox.Show("Basım evi boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                eviTxtBx.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(basimYili))
            {
                MessageBox.Show("Basım yılı boş bırakılamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                yiliTxtBx.Focus();
                return;
            }
            if (basimYili.Length != 4 || !int.TryParse(basimYili, out _)) 
            {
                MessageBox.Show("Basım yılı 4 haneli bir sayı olmalıdır (örn: 2023)!", "Geçersiz Veri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                yiliTxtBx.Focus();
                yiliTxtBx.SelectAll();
                return;
            }
            if (adet <= 0)
            {
                MessageBox.Show("Kitap adedi 0'dan büyük olmalıdır!", "Geçersiz Veri", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numericUpDownAdet.Focus();
                return;
            }  
            try
            {
                int sonucKodu = vi.KitapEkle(kitapAdi, yazar, basimEvi, basimYili, adet);

                switch (sonucKodu)
                {
                    case 0:
                        MessageBox.Show("Kitap başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        TemizleFormAlanlarini();
                        break;
                    case 1: 
                        MessageBox.Show("Kitap eklenirken bir veritabanı hatası oluştu.\nBu kitap zaten mevcut olabilir veya girilen bilgilerde bir sorun var.", "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 2: 
                        MessageBox.Show("Kitap eklenemedi. Lütfen bilgileri kontrol edin veya daha sonra tekrar deneyin.", "Ekleme Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    default:
                        MessageBox.Show("Kitap ekleme işlemi sırasında bilinmeyen bir durum oluştu.", "Bilinmeyen Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Kitap eklenirken programatik bir hata oluştu: {ex.Message}", "Program Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TemizleFormAlanlarini()
        {
            kitapAdTxtBx.Clear();
            yazarTxtBx.Clear();
            eviTxtBx.Clear();
            yiliTxtBx.Clear();
            numericUpDownAdet.Value = numericUpDownAdet.Minimum; 
            kitapAdTxtBx.Focus(); 
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void KitapEklemeFormu_Load(object sender, EventArgs e)
        {
            
        }
    }
}