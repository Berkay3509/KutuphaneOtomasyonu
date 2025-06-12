using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KullaniciListelemeFormu : Form
    {
       
        VeritabaniIslemleri vi = new VeritabaniIslemleri(); 

        public KullaniciListelemeFormu()
        {
            InitializeComponent();
        }

        private void KullaniciListelemeFormu_Load(object sender, EventArgs e)
        {
            try
            {
                
                DataTable dt = vi.SistemKullanicilariListesi();

                if (dt != null) 
                {
                    dataGridView1.DataSource = dt;

                   
                    if (dataGridView1.Columns.Contains("Sifre"))
                    {
                        dataGridView1.Columns["Sifre"].Visible = false;
                    }

               
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; 
                }
                else
                {
                    MessageBox.Show("Kullanıcı listesi yüklenirken bir sorun oluştu veya liste boş.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dataGridView1.DataSource = null; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcılar listelenirken beklenmedik bir hata oluştu: " + ex.Message, "Genel Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dataGridView1.DataSource = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}