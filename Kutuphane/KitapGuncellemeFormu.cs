using System;
using System.Data;
using System.Windows.Forms;


namespace Kutuphane
{
    public partial class KitapGuncellemeFormu : Form
    {
        VeritabaniIslemleri vi = new VeritabaniIslemleri();

        public KitapGuncellemeFormu()
        {
            InitializeComponent();
        }

        private void KitapGuncellemeFormu_Load(object sender, EventArgs e)
        {
            ComboboxDoldur();
            comboBox1.SelectedIndex = -1;
            TemizleFormAlanlarini();
        }

        void ComboboxDoldur()
        {
            try
            {
                DataTable dt = vi.KitaplarComboBoxListesi();

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.comboBox1.SelectedIndexChanged -= new System.EventHandler(this.comboBox1_SelectedIndexChanged);

                    comboBox1.DataSource = dt;
                    comboBox1.DisplayMember = "Adi";
                    comboBox1.ValueMember = "k_id";
                    this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
                    comboBox1.SelectedIndex = -1; 
                }
                else
                {
                    comboBox1.DataSource = null;
                    comboBox1.Items.Clear();
                    comboBox1.SelectedIndex = -1;
                    MessageBox.Show("Sistemde güncellenecek kitap bulunamadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kitap listesi ComboBox'a yüklenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedItem == null)
            {
                TemizleFormAlanlarini();
                return;
            }

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
                        numericUpDownAdet.Value = kacTane;
                    }
                    else
                    {
                        numericUpDownAdet.Value = 0;
                    }
                }
                else
                {
                    TemizleFormAlanlarini();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Seçili kitap bilgileri yüklenirken bir hata oluştu:\n" + ex.Message, "Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TemizleFormAlanlarini();
            }
        }
        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Lütfen güncellemek için bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(kitapAdTxtBx.Text) ||
                string.IsNullOrWhiteSpace(yazarTxtBx.Text) ||
                string.IsNullOrWhiteSpace(eviTxtBx.Text) ||
                string.IsNullOrWhiteSpace(yiliTxtBx.Text))
            {
                MessageBox.Show("Kitap adı, yazar, basım evi ve basım yılı alanları boş bırakılamaz.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string kitapAdi = kitapAdTxtBx.Text.Trim();
                string yazari = yazarTxtBx.Text.Trim();
                string basimEvi = eviTxtBx.Text.Trim();
                string basimYili = yiliTxtBx.Text.Trim();
                int kacTane = (int)numericUpDownAdet.Value;
                int secilenKitapId;

                if (!int.TryParse(comboBox1.SelectedValue.ToString(), out secilenKitapId))
                {
                    MessageBox.Show("Geçerli bir kitap ID'si alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (vi.KitapGuncelle(secilenKitapId, kitapAdi, yazari, basimEvi, basimYili, kacTane))
                {
                    MessageBox.Show("Kitap başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    string guncellenenKitapAdi = kitapAdi; 
                    int guncellenenKitapID = secilenKitapId;
                    ComboboxDoldur();
                    bool bulundu = false;
                    for (int i = 0; i < comboBox1.Items.Count; i++)
                    {
                        if (comboBox1.Items[i] is DataRowView drv)
                        {
                            if (Convert.ToInt32(drv["k_id"]) == guncellenenKitapID)
                            {
                                comboBox1.SelectedIndex = i;
                                bulundu = true;
                                break;
                            }
                        }
                    }
                    if (!bulundu && comboBox1.Items.Count > 0)
                    {
                        comboBox1.SelectedIndex = 0;
                    }
                    else if (!bulundu)
                    {
                        comboBox1.SelectedIndex = -1; 
                        TemizleFormAlanlarini();
                    }
                }
                else
                {
                    MessageBox.Show("Kitap güncellenemedi (belirtilen ID'de kitap bulunamadı, değişiklik yapılmadı veya bir veritabanı hatası oluştu).", "Güncelleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Kitap güncellenirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TemizleFormAlanlarini()
        {
            kitapAdTxtBx.Clear();
            yazarTxtBx.Clear();
            eviTxtBx.Clear();
            yiliTxtBx.Clear();
            numericUpDownAdet.Value = 0;
        }
    }
}