using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntrenmanProgrami
{
    class Egzersiz
    {
        private string isim;
        private int setSayisi;
        private int tekrarSayisi;
        private int agirlik;

        // güvenliği arttırmak için kapsülleme kullandım

        public string Isim { get { return isim; } set { this.isim = value; } }
        public int SetSayisi { get { return setSayisi; } set { this.setSayisi = value; } }
        public int TekrarSayisi { get { return tekrarSayisi; } set { this.tekrarSayisi = value; } }
        public int Agirlik { get { return agirlik; } set { this.agirlik = value; } }

        // verileri nesen oluştururken alabilmek için constracter kullandım
        public Egzersiz(string isim, int set, int tekrar, int agirlik)
        {
            this.isim = isim;
            this.setSayisi = set;
            this.tekrarSayisi = tekrar;
            this.agirlik = agirlik;
        }

        public string BilgiGoster()
        {
            return $"{isim} - {setSayisi}x{tekrarSayisi} - {agirlik} kg";
        }

        //karışıklık olmaması için dosyaya bu formatta kaydetmeyi tercih ettim
        public string DosyaFormat()
        {
            return $"{isim},{setSayisi},{tekrarSayisi},{agirlik}";
        }

        // Dosyayay kaydettiğim formata uygun okuma yapabilmek için , lerden sonra veriler ayrılmıştır
        public static Egzersiz DosyadanOku(string satir)
        {
            string[] parcalar = satir.Split(',');
            return new Egzersiz(parcalar[0], Convert.ToInt32(parcalar[1]), Convert.ToInt32(parcalar[2]), Convert.ToInt32(parcalar[3]));
        }
    }
}