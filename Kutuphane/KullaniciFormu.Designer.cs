namespace Kutuphane
{
    partial class KullaniciFormu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KullaniciFormu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ödünçListemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teslimEttiğimKitaplarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kütüphanedekiKitapListesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hakkındaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ödünçListemToolStripMenuItem,
            this.kütüphanedekiKitapListesiToolStripMenuItem,
            this.hakkındaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ödünçListemToolStripMenuItem
            // 
            this.ödünçListemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.teslimEttiğimKitaplarToolStripMenuItem,
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem});
            this.ödünçListemToolStripMenuItem.Name = "ödünçListemToolStripMenuItem";
            this.ödünçListemToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.ödünçListemToolStripMenuItem.Text = "Ödünç Listem";
            // 
            // teslimEttiğimKitaplarToolStripMenuItem
            // 
            this.teslimEttiğimKitaplarToolStripMenuItem.Name = "teslimEttiğimKitaplarToolStripMenuItem";
            this.teslimEttiğimKitaplarToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.teslimEttiğimKitaplarToolStripMenuItem.Text = "Teslim Ettiğim Kitaplar";
            this.teslimEttiğimKitaplarToolStripMenuItem.Click += new System.EventHandler(this.teslimEttiğimKitaplarToolStripMenuItem_Click);
            // 
            // henüzTeslimEtmediğimKitaplarToolStripMenuItem
            // 
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem.Name = "henüzTeslimEtmediğimKitaplarToolStripMenuItem";
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem.Size = new System.Drawing.Size(249, 22);
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem.Text = "Henüz Teslim Etmediğim Kitaplar";
            this.henüzTeslimEtmediğimKitaplarToolStripMenuItem.Click += new System.EventHandler(this.henüzTeslimEtmediğimKitaplarToolStripMenuItem_Click);
            // 
            // kütüphanedekiKitapListesiToolStripMenuItem
            // 
            this.kütüphanedekiKitapListesiToolStripMenuItem.Name = "kütüphanedekiKitapListesiToolStripMenuItem";
            this.kütüphanedekiKitapListesiToolStripMenuItem.Size = new System.Drawing.Size(164, 20);
            this.kütüphanedekiKitapListesiToolStripMenuItem.Text = "Kütüphanedeki Kitap Listesi";
            this.kütüphanedekiKitapListesiToolStripMenuItem.Click += new System.EventHandler(this.kütüphanedekiKitapListesiToolStripMenuItem_Click);
            // 
            // hakkındaToolStripMenuItem
            // 
            this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
            this.hakkındaToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.hakkındaToolStripMenuItem.Text = "Hakkında";
            this.hakkındaToolStripMenuItem.Click += new System.EventHandler(this.hakkındaToolStripMenuItem_Click);
            // 
            // KullaniciFormu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 385);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "KullaniciFormu";
            this.Load += new System.EventHandler(this.KullaniciFormu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ödünçListemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem teslimEttiğimKitaplarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem henüzTeslimEtmediğimKitaplarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kütüphanedekiKitapListesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hakkındaToolStripMenuItem;
    }
}