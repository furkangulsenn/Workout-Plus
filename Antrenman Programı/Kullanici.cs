using System;

namespace AntrenmanProgrami
{
    public class Kullanici
    {
        private string ad;
        private double boy;
        private double kilo;
        private string cinsiyet;
        private double vki;
        private int seviye;

        public string Ad { get { return ad; } set { ad = value; } }

        public double Boy{get { return boy; }set { boy = value; }}

        public double Kilo {get { return kilo; }set { kilo = value; }}

        public string Cinsiyet{get { return cinsiyet; }set { cinsiyet = value; }}

        public double VKI{get { return vki; }}

        public int Seviye{get { return seviye; }}


        public Kullanici(string ad, double boy, double kilo, string cinsiyet)
        {
            this.ad = ad;
            this.boy = boy;
            this.kilo = kilo;
            this.cinsiyet = cinsiyet;

            VKIHesapla();
            SeviyeBelirle();
        }

        private void VKIHesapla()
        {
            vki = kilo / (boy * boy);

            // Erkek - Kadın farklı değerlendirme
            if (cinsiyet == "Erkek")
            {
                vki = vki * 1.0;
            }
            else
            {
                vki = vki * 0.95;
            }
        }

        private void SeviyeBelirle()
        {
            if (vki < 18)
                seviye = 1;
            else if (vki < 25)
                seviye = 2;
            else
                seviye = 3;
        }

        public string KullaniciDosyaFormat()
        {
            return ad + ";" + boy + ";" + kilo + ";" + cinsiyet;
        }


        public string SeviyeDosyaFormat()
        {
            return ad + ";" + vki + ";" + seviye;
        }

        public static Kullanici DosyadanOku(string satir)
        {
            string[] p = satir.Split(';');

            Kullanici k = new Kullanici(
                p[0],
                Convert.ToDouble(p[1]),
                Convert.ToDouble(p[2]),
                p[3]
            );

            return k;
        }
    }
}