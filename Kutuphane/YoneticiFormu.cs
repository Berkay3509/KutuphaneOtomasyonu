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
    public partial class YoneticiFormu : Form
    {
        public YoneticiFormu()
        {
            InitializeComponent();
        }

        private void kitapSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapGuncellemeFormu frm = new KitapGuncellemeFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void üyeEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            UyeEklemeFormu frm = new UyeEklemeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void YoneticiFormu_Load(object sender, EventArgs e)
        {

        }

        private void üyeGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            UyeGuncellemeFormu frm = new UyeGuncellemeFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void üyeSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            UyeSilmeFormu frm = new UyeSilmeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void üyeListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            UyeListelemeFormu frm = new UyeListelemeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void kullanıcıGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciGuncellemeFormu frm = new KullaniciGuncellemeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void kullanıcıListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KullaniciListelemeFormu frm = new KullaniciListelemeFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void kitapEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapEklemeFormu frm = new KitapEklemeFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void kitapSilToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapSilmeFormu frm = new KitapSilmeFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(); 
        }

        private void kitapListeleToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void kitapVerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapVerFormu frm = new KitapVerFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void kitapAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            KitapAlFormu frm = new KitapAlFormu();
            frm.MdiParent = this; 
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }

        private void alınanKitaplarıListeleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            AlinanKitaplariListeleFormu frm = new AlinanKitaplariListeleFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized; 
            frm.Show(); 
        }
        private void verilenKitaplarıListeleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            foreach (Form item in this.MdiChildren)
            {
                item.Close();
            }
            VerilenKitaplariListeleFormu frm = new VerilenKitaplariListeleFormu();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show(); 
        }

        private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HakkindaFormu hakkindaFormu = new HakkindaFormu();
            hakkindaFormu.ShowDialog();
        }
    }
}
