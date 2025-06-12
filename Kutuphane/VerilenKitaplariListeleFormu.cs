using System;
using System.Data;
using System.Windows.Forms;


namespace Kutuphane
{
    public partial class VerilenKitaplariListeleFormu : Form
    {
      
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public VerilenKitaplariListeleFormu()
        {
            InitializeComponent();
        }

        private void VerilenKitaplariListeleFormu_Load(object sender, EventArgs e)
        {
            DoldurVerilenKitaplarDataGridView();
        }

        private void DoldurVerilenKitaplarDataGridView()
        {
            try
            {
              
                DataTable dt = vi.OduncAlinmisKitaplarListesii();

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Sistemde şu anda ödünç verilmiş ve teslim edilmemiş kitap bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Verilen kitaplar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}