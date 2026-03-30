using Antrenman_Programı;
using AntrenmanProgrami;
using System;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AntrenmanProgrami
{
    public partial class FormKayit : Form
    {
        string dosya = "kullanicilar.txt";

        public FormKayit()
        {
            InitializeComponent();
        }

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            try
            {
                string ad = txtIsim.Text;
                double boy = Convert.ToDouble(txtBoy.Text);
                double kilo = Convert.ToDouble(txtKilo.Text);
                string cinsiyet = cmbCinsiyet.Text;

                Kullanici k = new Kullanici(ad, boy, kilo, cinsiyet);

                // Kullanıcı bilgileri
                StreamWriter sw1 = new StreamWriter("kullanicilar.txt", true);
                sw1.WriteLine(k.KullaniciDosyaFormat());
                sw1.Close();

                // Seviye bilgileri
                StreamWriter sw2 = new StreamWriter("seviyeler.txt", true);
                sw2.WriteLine(k.SeviyeDosyaFormat());
                sw2.Close();

                MessageBox.Show("Kayıt başarılı!");

                Form1 frm = new Form1();
                frm.Show();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Hatalı giriş!");
            }

        }
    }
}

