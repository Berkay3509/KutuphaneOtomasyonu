using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KullaniciAlinanKitaplariListeleFormu : Form
    {
        public int uye_no = 0;
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KullaniciAlinanKitaplariListeleFormu()
        {
            InitializeComponent();
        }

        private void KullaniciAlinanKitaplariListeleFormu_Load(object sender, EventArgs e)
        {
            if (this.uye_no > 0)
            {
                DoldurKullanicininTeslimEttigiKitaplarDataGridView();
            }
            else
            {
                MessageBox.Show("Geçerli bir üye numarası forma aktarılmadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
                this.Close(); // Formu kapat
            }
        }

        private void DoldurKullanicininTeslimEttigiKitaplarDataGridView()
        {
            try
            {
                DataTable dt = vi.KullanicininTeslimEttigiKitaplarListesi(this.uye_no);

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    // k_id veya KitapID gibi sütunları gizlemek isterseniz:
                    // Sorgunuzda Kitaplar.k_id'yi KitapID olarak aliasladıysanız:
                    // if (dataGridView1.Columns.Contains("KitapID"))
                    // {
                    //     dataGridView1.Columns["KitapID"].Visible = false;
                    // }
                    // Ya da doğrudan k_id varsa (sorgunuzda k_id geçmiyor, Uye_No ve KacTane geçiyor):
                    // if (dataGridView1.Columns.Contains("k_id"))
                    // {
                    //     dataGridView1.Columns["k_id"].Visible = false;
                    // }

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Bu üyenin daha önce teslim ettiği bir kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Teslim edilen kitap listesi yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

       
    }
}