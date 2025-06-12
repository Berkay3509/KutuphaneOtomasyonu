using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KitapListelemeFormu : Form
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KitapListelemeFormu()
        {
            InitializeComponent();
        }

        private void KitapListelemeFormu_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtKitaplar = vi.TumKitaplarListesi();

                if (dtKitaplar != null)
                {
                    dataGridView1.DataSource = dtKitaplar;

                    if (dataGridView1.Columns.Contains("[İD]"))
                    {
                        dataGridView1.Columns["[İD]"].Visible = false;
                    }
                    else if (dataGridView1.Columns.Count > 0 && dataGridView1.Columns[0].HeaderText == "İD") 
                    {
                        dataGridView1.Columns[0].Visible = false;
                    }
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    if (dtKitaplar.Rows.Count == 0)
                    {
                        MessageBox.Show("Sistemde kayıtlı kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Kitap listesi yüklenirken bir sorun oluştu veya liste boş.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }
    }
}