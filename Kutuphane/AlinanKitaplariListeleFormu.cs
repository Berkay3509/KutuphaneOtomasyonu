using System;
using System.Data;
using System.Windows.Forms;


namespace Kutuphane
{
    public partial class AlinanKitaplariListeleFormu : Form
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public AlinanKitaplariListeleFormu()
        {
            InitializeComponent();
        }

        private void AlinanKitaplariListeleFormu_Load(object sender, EventArgs e)
        {
            TeslimEdilmisKitaplariYukle();

          
            if (dataGridView1 != null && dataGridView1.DataSource != null)
            {
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Dock = DockStyle.Fill; // Bu genellikle tasarımcıdan ayarlanır.
                dataGridView1.MultiSelect = false;
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
        }

        private void TeslimEdilmisKitaplariYukle()
        {
            try
            {
                DataTable dt = vi.TumTeslimEdilmisKitaplarListesi();

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;

                    // Gerekli sütunları gizle
                    // VeritabaniIslemleri'ndeki sorguda KitapID alias'ı ekledim, ona göre kontrol edelim.
                    if (dataGridView1.Columns.Contains("Uye_No"))
                        dataGridView1.Columns["Uye_No"].Visible = false;
                    if (dataGridView1.Columns.Contains("KitapID"))
                        dataGridView1.Columns["KitapID"].Visible = false;
                    if (dataGridView1.Columns.Contains("KacTane"))
                        dataGridView1.Columns["KacTane"].Visible = false;

                    // Otomatik boyutlandırma (Eğer Form_Load'da ayarlanmadıysa)
                    if (dataGridView1.AutoSizeColumnsMode != DataGridViewAutoSizeColumnsMode.Fill)
                    {
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }


                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Sistemde daha önce teslim edilmiş kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Teslim edilmiş kitap listesi yüklenirken bir sorun oluştu veya liste boş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Teslim edilmiş kitaplar yüklenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }
    }
}