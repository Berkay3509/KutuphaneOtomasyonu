using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class UyeListelemeFormu : Form
    {
        public UyeListelemeFormu()
        {
            InitializeComponent();
        }

        private void UyeListelemeFormu_Load(object sender, EventArgs e)
        {
            VeritabaniIslemleri vi = new VeritabaniIslemleri();
            dataGridView1.DataSource = vi.YoneticiUyeListesi();
            dataGridView1.Columns[0].Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
