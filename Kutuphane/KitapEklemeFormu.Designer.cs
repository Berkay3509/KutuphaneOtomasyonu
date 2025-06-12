namespace Kutuphane
{
    partial class KitapEklemeFormu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KitapEklemeFormu));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.yazarTxtBx = new System.Windows.Forms.TextBox();
            this.eviTxtBx = new System.Windows.Forms.TextBox();
            this.yiliTxtBx = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.acceptButton = new System.Windows.Forms.Button();
            this.kitapAdTxtBx = new System.Windows.Forms.TextBox();
            this.numericUpDownAdet = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kitabın Adı :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Kitabın Yazarı :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Kitabın Basım Yılı :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(185, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Kitabın Basım Evi :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(185, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Kitabın Adeti :";
            // 
            // yazarTxtBx
            // 
            this.yazarTxtBx.Location = new System.Drawing.Point(283, 88);
            this.yazarTxtBx.Name = "yazarTxtBx";
            this.yazarTxtBx.Size = new System.Drawing.Size(100, 20);
            this.yazarTxtBx.TabIndex = 2;
            // 
            // eviTxtBx
            // 
            this.eviTxtBx.Location = new System.Drawing.Point(285, 142);
            this.eviTxtBx.Name = "eviTxtBx";
            this.eviTxtBx.Size = new System.Drawing.Size(100, 20);
            this.eviTxtBx.TabIndex = 3;
            // 
            // yiliTxtBx
            // 
            this.yiliTxtBx.Location = new System.Drawing.Point(283, 185);
            this.yiliTxtBx.Name = "yiliTxtBx";
            this.yiliTxtBx.Size = new System.Drawing.Size(100, 20);
            this.yiliTxtBx.TabIndex = 4;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(46, 281);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "İPTAL";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // acceptButton
            // 
            this.acceptButton.Location = new System.Drawing.Point(504, 281);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "EKLE";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.acceptButton_Click);
            // 
            // kitapAdTxtBx
            // 
            this.kitapAdTxtBx.Location = new System.Drawing.Point(285, 50);
            this.kitapAdTxtBx.Name = "kitapAdTxtBx";
            this.kitapAdTxtBx.Size = new System.Drawing.Size(100, 20);
            this.kitapAdTxtBx.TabIndex = 1;
            // 
            // numericUpDownAdet
            // 
            this.numericUpDownAdet.Location = new System.Drawing.Point(265, 243);
            this.numericUpDownAdet.Name = "numericUpDownAdet";
            this.numericUpDownAdet.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAdet.TabIndex = 5;
            // 
            // KitapEklemeFormu
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.numericUpDownAdet);
            this.Controls.Add(this.acceptButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.yiliTxtBx);
            this.Controls.Add(this.eviTxtBx);
            this.Controls.Add(this.yazarTxtBx);
            this.Controls.Add(this.kitapAdTxtBx);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "KitapEklemeFormu";
            this.Text = "KitapEklemeFormu";
            this.Load += new System.EventHandler(this.KitapEklemeFormu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAdet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox yazarTxtBx;
        private System.Windows.Forms.TextBox eviTxtBx;
        private System.Windows.Forms.TextBox yiliTxtBx;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.TextBox kitapAdTxtBx;
        private System.Windows.Forms.NumericUpDown numericUpDownAdet;
    }
}