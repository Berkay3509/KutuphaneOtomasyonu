namespace Kutuphane
{
    partial class KitapVerUyeSecFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KitapVerUyeSecFormu));
            this.uyeEposta = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uyeSoyadi = new System.Windows.Forms.TextBox();
            this.uyeAdi = new System.Windows.Forms.TextBox();
            this.btnKitabiVer = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uyeEposta
            // 
            this.uyeEposta.FormattingEnabled = true;
            this.uyeEposta.Location = new System.Drawing.Point(158, 12);
            this.uyeEposta.Name = "uyeEposta";
            this.uyeEposta.Size = new System.Drawing.Size(121, 21);
            this.uyeEposta.TabIndex = 0;
            this.uyeEposta.SelectedIndexChanged += new System.EventHandler(this.uyeEposta_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Eposta Adresi :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Soyadı :";
            // 
            // uyeSoyadi
            // 
            this.uyeSoyadi.Enabled = false;
            this.uyeSoyadi.Location = new System.Drawing.Point(158, 65);
            this.uyeSoyadi.Name = "uyeSoyadi";
            this.uyeSoyadi.Size = new System.Drawing.Size(121, 20);
            this.uyeSoyadi.TabIndex = 3;
            // 
            // uyeAdi
            // 
            this.uyeAdi.Enabled = false;
            this.uyeAdi.Location = new System.Drawing.Point(158, 39);
            this.uyeAdi.Name = "uyeAdi";
            this.uyeAdi.Size = new System.Drawing.Size(121, 20);
            this.uyeAdi.TabIndex = 4;
            // 
            // btnKitabiVer
            // 
            this.btnKitabiVer.Location = new System.Drawing.Point(27, 119);
            this.btnKitabiVer.Name = "btnKitabiVer";
            this.btnKitabiVer.Size = new System.Drawing.Size(75, 23);
            this.btnKitabiVer.TabIndex = 5;
            this.btnKitabiVer.Text = "KİTABI VER";
            this.btnKitabiVer.UseVisualStyleBackColor = true;
            this.btnKitabiVer.Click += new System.EventHandler(this.btnKitabiVer_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(297, 119);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "İPTAL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Adı :";
            // 
            // KitapVerUyeSecFormu
            // 
            this.AcceptButton = this.btnKitabiVer;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnKitabiVer);
            this.Controls.Add(this.uyeAdi);
            this.Controls.Add(this.uyeSoyadi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uyeEposta);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KitapVerUyeSecFormu";
            this.Text = "Üye Seç";
            this.Load += new System.EventHandler(this.KitapVerUyeSecFormu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox uyeEposta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox uyeSoyadi;
        private System.Windows.Forms.TextBox uyeAdi;
        private System.Windows.Forms.Button btnKitabiVer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
    }
}