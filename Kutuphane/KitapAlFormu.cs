using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KitapAlFormu : Form 
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KitapAlFormu()
        {
            InitializeComponent();
           
        }

        private void KitapAlFormu_Load_1(object sender, EventArgs e) 
        {
            ListeleOduncAlinmisKitaplar();
        }

        private void ListeleOduncAlinmisKitaplar()
        {
            try
            {
                DataTable dt = vi.OduncAlinmisKitaplarListesi();

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;

                   
                    if (dataGridView1.Columns.Contains("k_id"))
                        dataGridView1.Columns["k_id"].Visible = false;
                    if (dataGridView1.Columns.Contains("Uye_No"))
                        dataGridView1.Columns["Uye_No"].Visible = false;

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Sistemde şu anda ödünç alınmış ve teslim edilmemiş kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Ödünçteki kitap listesi yüklenirken bir sorun oluştu veya liste boş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow seciliSatir = dataGridView1.Rows[e.RowIndex];

               
                object kitapIdObj = seciliSatir.Cells["k_id"].Value;
                object uyeNoObj = seciliSatir.Cells["Uye_No"].Value;
                string kitapAdi = seciliSatir.Cells["Kitap Adı"].Value?.ToString() ?? "Bilinmiyor"; 
                string uyeAdi = seciliSatir.Cells["Üye Adı"].Value?.ToString() ?? "";
                string uyeSoyadi = seciliSatir.Cells["Üye Soyadı"].Value?.ToString() ?? "";


                if (kitapIdObj == null || uyeNoObj == null)
                {
                    MessageBox.Show("Seçili satırda gerekli ID bilgileri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(kitapIdObj.ToString(), out int kitapId) || !int.TryParse(uyeNoObj.ToString(), out int uyeNo))
                {
                    MessageBox.Show("Kitap veya Üye ID'si geçerli bir sayı değil.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                DialogResult sonuc = MessageBox.Show(
                    $"'{kitapAdi}' adlı kitabı, '{uyeAdi} {uyeSoyadi}' adlı üyeden iade almak istiyor musunuz?",
                    "Kitap İade Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (sonuc == DialogResult.Yes)
                {
                    string teslimTarihi = DateTime.Now.ToString("dd.MM.yyyy"); 
                    int iadeSonucKodu = vi.KitapIadeEt(kitapId, uyeNo, teslimTarihi);

                    switch (iadeSonucKodu)
                    {
                        case 0:
                            MessageBox.Show("Kitap başarıyla iade alındı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListeleOduncAlinmisKitaplar(); 
                            break;
                        case 1:
                            MessageBox.Show("İade işlemi yapılamadı: Ödünç kaydı bulunamadı veya kitap zaten iade edilmiş.", "İade Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ListeleOduncAlinmisKitaplar();
                            break;
                        case 2:
                            MessageBox.Show("İade işlemi sırasında stok güncellenemedi (kitap bulunamadı).\nVeritabanında tutarsızlık oluşmuş olabilir. Lütfen sistem yöneticisine başvurun!", "Stok Güncelleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ListeleOduncAlinmisKitaplar();
                            break;
                        case 3: 
                            MessageBox.Show("Kitap iade edilirken bir veritabanı veya genel hata oluştu. Lütfen sistem yöneticisine başvurun.", "İade Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Kitap iade işlemi sırasında bilinmeyen bir durum oluştu.", "Bilinmeyen Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }
    }
}