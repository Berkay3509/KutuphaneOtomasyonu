using System;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class KitapSilmeFormu : Form 
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KitapSilmeFormu()
        {
            InitializeComponent();
        }

        private void KitapSilmeFormu_Load(object sender, EventArgs e)
        {
            ComboboxDoldur();
            comboBox1.SelectedIndex = -1;
            TemizleFormAlanlarini();
            numericUpDownAdet.Minimum = 1;
            numericUpDownAdet.Value = 1;
        }

        void ComboboxDoldur()
        {
            try
            {
                DataTable dtKitaplar = vi.KitaplarComboBoxListesi();
                if (dtKitaplar != null)
                {
                    this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.comboBox1_SelectedIndexChanged);
                    comboBox1.DataSource = dtKitaplar;
                    comboBox1.DisplayMember = "Adi";
                    comboBox1.ValueMember = "k_id";
                    this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
                    comboBox1.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Kitap listesi yüklenemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitaplar ComboBox'a yüklenirken hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox1.SelectedValue != null)
            {
                try
                {
                    if (comboBox1.SelectedItem is DataRowView selectedRowView)
                    {
                        kitapAdTxtBx.Text = selectedRowView["Adi"].ToString();
                        yazarTxtBx.Text = selectedRowView["Yazari"].ToString();
                        eviTxtBx.Text = selectedRowView["BasimEvi"].ToString();
                        yiliTxtBx.Text = selectedRowView["BasimYili"].ToString();
                        if (selectedRowView["KacTane"] != DBNull.Value && int.TryParse(selectedRowView["KacTane"].ToString(), out int kacTane))
                        {
                            numericUpDownAdet.Maximum = kacTane > 0 ? kacTane : (numericUpDownAdet.Minimum > 0 ? numericUpDownAdet.Minimum : 1);

                            int atanacakDeger;
                            if (kacTane > 0)
                            {
                                atanacakDeger = 1;
                            }
                            else
                            {
                                atanacakDeger = 0; 
                            }
                            if (atanacakDeger < numericUpDownAdet.Minimum)
                            {
                                numericUpDownAdet.Value = numericUpDownAdet.Minimum;
                            }
                            else if (atanacakDeger > numericUpDownAdet.Maximum)
                            {
                                numericUpDownAdet.Value = numericUpDownAdet.Maximum;
                            }
                            else
                            {
                                numericUpDownAdet.Value = atanacakDeger;
                            }
                        }
                        else
                        {
                            numericUpDownAdet.Value = numericUpDownAdet.Minimum;
                            numericUpDownAdet.Maximum = numericUpDownAdet.Minimum > 0 ? numericUpDownAdet.Minimum : 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Seçili kitap bilgileri yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TemizleFormAlanlarini();
                }
            }
            else
            {
                TemizleFormAlanlarini();
            }
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Lütfen stok adedini azaltmak için bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int kitapIdToUpdate;
            if (!int.TryParse(comboBox1.SelectedValue.ToString(), out kitapIdToUpdate))
            {
                MessageBox.Show("Geçerli bir kitap ID'si seçilemedi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int azaltilacakAdet = (int)numericUpDownAdet.Value;
            if (azaltilacakAdet <= 0)
            {
                MessageBox.Show("Lütfen azaltmak için geçerli bir adet girin (0'dan büyük).", "Geçersiz Adet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string seciliKitapAdi = kitapAdTxtBx.Text;

            DialogResult sonuc = MessageBox.Show(
                $"'{seciliKitapAdi}' adlı kitaptan {azaltilacakAdet} adet silmek (stoğu azaltmak) istediğinize emin misiniz?",
                "Stok Azaltma Onayı",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (sonuc == DialogResult.Yes)
            {
                try
                {
                    int sonucKodu = vi.KitapStokAzalt(kitapIdToUpdate, azaltilacakAdet, false);

                    switch (sonucKodu)
                    {
                        case 0:
                            MessageBox.Show($"{azaltilacakAdet} adet kitap başarıyla stoktan düşüldü.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ComboboxDoldur();
                            if (comboBox1.Items.Count > 0)
                            {
                                bool bulundu = false;
                                for (int i = 0; i < comboBox1.Items.Count; i++)
                                {
                                    if (comboBox1.Items[i] is DataRowView drv && Convert.ToInt32(drv["k_id"]) == kitapIdToUpdate)
                                    {
                                        comboBox1.SelectedIndex = i;
                                        bulundu = true;
                                        break;
                                    }
                                }
                                if (!bulundu) TemizleFormAlanlarini();
                            }
                            else
                            {
                                TemizleFormAlanlarini();
                            }
                            break;
                        case 1:
                            MessageBox.Show("Kitap bulunamadı veya stokta yeterli sayıda kitap yok.", "Stok Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 2:
                            MessageBox.Show("Kitap stok adedi güncellenirken bir veritabanı hatası oluştu.", "Güncelleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 3:
                            MessageBox.Show("Stok azaltıldı ancak kitap silinirken bir sorun oluştu (eğer silme seçeneği aktifse).", "Silme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        default:
                            MessageBox.Show("Bilinmeyen bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("İşlem sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TemizleFormAlanlarini()
        {
            kitapAdTxtBx.Clear();
            yazarTxtBx.Clear();
            eviTxtBx.Clear();
            yiliTxtBx.Clear();
            numericUpDownAdet.Value = numericUpDownAdet.Minimum;
        }
    }
}