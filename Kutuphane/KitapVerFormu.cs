using System;
using System.Data;

using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KitapVerFormu : Form
    {
      
        VeritabaniIslemleri vi = new VeritabaniIslemleri(); 

        public KitapVerFormu()
        {
            InitializeComponent();
        }

        private void KitapVerFormu_Load(object sender, EventArgs e)
        {
            KitaplariDataGridVieweYukle();
        }

        private void KitaplariDataGridVieweYukle()
        {
            try
            {
                
                DataTable dt = vi.StoktakiKitaplarListesi();

                if (dt != null) // Metot null dönebilir (hata durumunda)
                {
                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Contains("Adi"))
                        dataGridView1.Columns["Adi"].HeaderText = "Kitap Adı";
                    if (dataGridView1.Columns.Contains("Yazari"))
                        dataGridView1.Columns["Yazari"].HeaderText = "Yazarı";
                    if (dataGridView1.Columns.Contains("BasimEvi"))
                        dataGridView1.Columns["BasimEvi"].HeaderText = "Basım Evi";
                    if (dataGridView1.Columns.Contains("BasimYili"))
                        dataGridView1.Columns["BasimYili"].HeaderText = "Basım Yılı";
                    if (dataGridView1.Columns.Contains("KacTane"))
                        dataGridView1.Columns["KacTane"].HeaderText = "Stok Adedi";

                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Stokta ödünç verilebilecek kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Kitap listesi yüklenirken bir sorun oluştu veya liste boş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null; // Hata veya boş liste durumunda grid'i temizle
                }
            }
           
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar yüklenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow seciliSatir = dataGridView1.SelectedRows[0];
                KitapVerUyeSecFormu frm = new KitapVerUyeSecFormu();

                try
                {
                    frm.Selected_KitapAdi = seciliSatir.Cells["Adi"].Value.ToString();
                    frm.Selected_KitapYazari = seciliSatir.Cells["Yazari"].Value.ToString();
                    frm.Selected_KitapBasimEvi = seciliSatir.Cells["BasimEvi"].Value.ToString();
                    frm.Selected_KitapBasimYili = seciliSatir.Cells["BasimYili"].Value.ToString();
                    frm.Selected_KacTane = Convert.ToInt32(seciliSatir.Cells["KacTane"].Value);

                    DialogResult sonuc = frm.ShowDialog(this); // this, bu formun KitapVerUyeSecFormu'nun sahibi olduğunu belirtir

                    
                    if (sonuc == DialogResult.OK)
                    {
                        KitaplariDataGridVieweYukle();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Seçili kitap bilgileri alınırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}