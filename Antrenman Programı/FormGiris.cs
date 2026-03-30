using AntrenmanProgrami;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Antrenman_Programı
{
    public partial class FormGiris : Form
    {

        public FormGiris()
        {
            InitializeComponent();
            BasligiGuncelle();
        }



        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (File.Exists("kullanicilar.txt"))
            {
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else
            {
                FormKayit frm = new FormKayit();
                frm.Show();
                this.Hide();
            }
        }

        private void BasligiGuncelle()
        {
            if (!File.Exists("kullanicilar.txt"))
                return;

            string[] satirlar = File.ReadAllLines("kullanicilar.txt");

            if (satirlar.Length == 0)
                return;

            string sonSatir = satirlar[satirlar.Length - 1];
            string[] parca = sonSatir.Split(';');

            string cinsiyet = parca[3];
            string cnsyt;

            if (cinsiyet == "Erkek") cnsyt = "Bey";
          
            else cnsyt = "Hanım";


            string ad = parca[0];

            lblBaslik.Text = "Antrenman Programına Hoşgeldin " + ad + " " + cnsyt;
        }
    }
}
