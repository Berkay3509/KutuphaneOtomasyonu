using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KullaniciFormu : Form
    {
        public KullaniciFormu()
        {
            InitializeComponent();
        }
        public string kullaniciAdi = "";
        public int uye_no = 0;
        private void KullaniciFormu_Load(object sender, EventArgs e)
        {
            this.Text += "Merhaba " + kullaniciAdi;
        }

        private void teslimEttiğimKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciAlinanKitaplariListeleFormu frm = new KullaniciAlinanKitaplariListeleFormu();
            frm.uye_no = uye_no;
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show();
        }

        private void henüzTeslimEtmediğimKitaplarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciVerilenKitaplariListeleFormu frm = new KullaniciVerilenKitaplariListeleFormu();
            frm.uye_no = uye_no;
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show();
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HakkindaFormu frm = new HakkindaFormu();
            frm.ShowDialog();
        }

        private void kütüphanedekiKitapListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapListelemeFormu frm = new KitapListelemeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }
    }
}
