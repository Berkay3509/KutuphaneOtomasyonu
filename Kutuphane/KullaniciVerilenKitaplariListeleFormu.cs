using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KullaniciVerilenKitaplariListeleFormu : Form
    {
        public int uye_no = 0;
        VeritabaniIslemleri vi = new VeritabaniIslemleri(); 

        public KullaniciVerilenKitaplariListeleFormu()
        {
            InitializeComponent();
           
        }

        private void KullaniciVerilenKitaplariListeleFormu_Load(object sender, EventArgs e)
        {
            if (this.uye_no > 0)
            {
                DoldurKullanicininVerilenKitaplarDataGridView();
            }
            else
            {
                MessageBox.Show("Geçerli bir üye numarası forma aktarılmadı. Lütfen formu tekrar açmayı deneyin veya bir üye seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void DoldurKullanicininVerilenKitaplarDataGridView()
        {
            try
            {
                // VeritabaniIslemleri sınıfındaki yeni metodu çağır
                DataTable dt = vi.KullanicininVerilenKitaplariListesi(this.uye_no);

                dataGridView1.DataSource = dt;
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dt == null || dt.Rows.Count == 0) // DataTable null olabilir veya satır içermeyebilir
                {
                    // Opsiyonel: Kullanıcıya bilgi mesajı
                    // MessageBox.Show("Bu üyenin şu anda ödünç aldığı ve teslim etmediği bir kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) // VeritabaniIslemleri içindeki hata yakalama yetersizse veya hata yukarı fırlatıldıysa
            {
                MessageBox.Show("Kitaplar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
           
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Gerekirse doldurulacak
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Gerekirse doldurulacak
        }

        
        private void KullaniciVerilenKitaplariListeleFormu_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
        
    }
}