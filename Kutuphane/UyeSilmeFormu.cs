using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class UyeSilmeFormu : Form
    {
        public UyeSilmeFormu()
        {
            InitializeComponent();
        }
        VeritabaniIslemleri vi = new VeritabaniIslemleri();
        private void UyeSilmeFormu_Load(object sender, EventArgs e)
        {
            DoldurUyeComboBox();
            comboBox1.SelectedIndex = -1;
            TemizleFormAlanlarini(); 
        }

        void DoldurUyeComboBox()
        {
            comboBox1.DataSource = new BindingSource(vi.YoneticiComboboxDataTable(), null);
            comboBox1.DisplayMember = "Üye E-Posta Adresi";
            comboBox1.ValueMember = "Uye_No";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex != -1)
            {
                string[] sonuc = vi.YoneticiUyeNoyaGoreUyeBilgisi(comboBox1.SelectedValue.ToString());
                if (sonuc[1] != "-1")
                {
                    try
                    {
                        adTxtBx.Text = sonuc[1];
                        soyadiTxtBx.Text = sonuc[2];
                        epostaTxtBx.Text = sonuc[3];
                        telTxtBx.Text = sonuc[4];
                        dateTimePicker1.Value = DateTime.Parse(sonuc[5]); 
                        dateTimePicker2.Value = DateTime.Parse(sonuc[6]); 
                        richTextBox1.Text = sonuc[7];
                    }
                    catch
                    { }
                }
                else
                {
                    MessageBox.Show("Veritabanı bağlantı hatası oluştu.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TemizleFormAlanlarini()
        {
            adTxtBx.Text = "";
            soyadiTxtBx.Text = "";
            telTxtBx.Text = "";
            epostaTxtBx.Text = "";
            richTextBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Checked = false; 
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker2.Checked = false; 
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Lütfen silmek için bir üye seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show("Üye Adı : " + adTxtBx.Text + "\nÜye Soyadı : " + soyadiTxtBx.Text +
                "\n olan üyeyi silmek istediğinize emin misiniz?\n(Silme işlemi üyenin daha önceden aldığı kitaplara ait bilgileri ve " +
                "üyeye ait kullanıcı bilgisini de silecektir.))", "Dikkat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dr == DialogResult.Yes)
            {
                if (vi.YoneticiUyeSilme(comboBox1.SelectedValue.ToString()))
                {
                    MessageBox.Show("Üye başarılı bir şekilde silindi", "Silindi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DoldurUyeComboBox();
                }
                else
                {
                    MessageBox.Show("Veritabanı bağlantı hatası oluştu.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}