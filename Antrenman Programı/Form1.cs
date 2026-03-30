using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AntrenmanProgrami
{
    public partial class Form1 : Form
    {
        ArrayList egzersizListesi = new ArrayList(); // listboxa kaydetmek için nesne oluşturdum
        string dosyaAdi = "egzersizler.txt"; // string dosyaAdi = @"C:\Users\Furkan\Desktop\egzersizler.txt"; gibi bir yol vermediğim için debug dosyası içine kaydetti

        public Form1()
        {
            InitializeComponent();
            VerileriYukle(); // Program açıldığında verileri yüklemek için buraya ekledim
            ProgramiGoster();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Buton çalıştı");

            try
            {
                string isim = txtAd.Text;


                int set = Convert.ToInt32(txtSet.Text);
                int tekrar = Convert.ToInt32(txtTekrar.Text);
                int agirlik = Convert.ToInt32(txtAgirlik.Text);

                if (set <= 0)
                {
                    MessageBox.Show("Set sayısı negatif olamaz!");
                    return;
                }
                if (tekrar <= 0)
                {
                    MessageBox.Show("Tekrar sayısı negatif olamaz!");
                    return;
                }
                if (agirlik < 0)
                {
                    MessageBox.Show("Ağırlık negatif olamaz!");
                    return;
                }

                if (isim == "")
                {
                    MessageBox.Show("Egzersiz adı boş bırakılamaz!");
                    return;
                }

                Egzersiz yeni = new Egzersiz(isim, set, tekrar, agirlik);
                egzersizListesi.Add(yeni);

                Guncelle();      // Liste ve grafik güncelle
                DosyayaKaydet(); // Dosyaya kaydet

                txtAd.Clear(); txtSet.Clear(); txtTekrar.Clear(); txtAgirlik.Clear();
            }
            catch
            {
                MessageBox.Show("Lütfen sayı olan alanlara doğru değer girin!");
            }
        }

        private void Guncelle()
        {
            // Listeyi temizle ve yeniden eklemek için sebebi dosyada olan verilerin aynısını bidaha yazmasını engellemek için yaptım

            listBox1.Items.Clear();
            chart1.Series.Clear();

            //chartta ağırlık bölümü ekleyebilmek için series kullanıldı ve ayarlamalar yapıldı

            Series s = chart1.Series.Add("Ağırlık");
            s.ChartType = SeriesChartType.Column;

            for (int i = 0; i < egzersizListesi.Count; i++)
            {
                Egzersiz eg = (Egzersiz)egzersizListesi[i]; // Egzersiz classı tipinde bi değer oluşturmak için yaptık sebebi zaten onun içindeki değerleri 'eg' değişkeni kullanılarak elde edilmek istenmesidir Egzersiz klassının içinde int ve stringler blundupu için hepsini kapsayan bi tiptir.
                listBox1.Items.Add(eg.BilgiGoster()); //array listin i. indeksinden aldığım bilgileri egzersiz class ı içindeki metodla listboxa yazdırmak için yaptım

                s.Points.AddXY(eg.Isim, eg.Agirlik); //grafiğe eklemek için
            }
        }

        private void DosyayaKaydet()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(dosyaAdi))   //StreamWriter dosyayı açar , dosya yoksa oluşturur, varsa üzerine yazar , using bloğu bittiğinde dosya otomatik kapanır.
                {
                    for (int i = 0; i < egzersizListesi.Count; i++)
                    {
                        Egzersiz eg = (Egzersiz)egzersizListesi[i];
                        sw.WriteLine(eg.DosyaFormat());
                    }
                }
            }
            catch
            {
                MessageBox.Show("Dosyaya yazarken bir hata oluştu!");
            }
        }

        private void VerileriYukle()
        {
            try
            {
                if (!File.Exists(dosyaAdi)) return;

                egzersizListesi.Clear();

                using (StreamReader sr = new StreamReader(dosyaAdi))
                {
                    while (!sr.EndOfStream)
                    {
                        string satir = sr.ReadLine();
                        Egzersiz eg = Egzersiz.DosyadanOku(satir);
                        egzersizListesi.Add(eg);
                    }
                }
                Guncelle();
            }
            catch
            {
                MessageBox.Show("Dosyadan veriler yüklenirken hata oluştu!");
            }
        }

        private void btnOrtalama_Click(object sender, EventArgs e)
        {

            if (egzersizListesi.Count == 0)
            {
                MessageBox.Show("Liste boş, ortalama hesaplanamaz!");
                return;
            }


            double toplamAgirlik = 0;
            int adet = 0;  // Sadece ağırlığı > 0 olanların sayısı

            // Sadece ağırlığı pozitif olanları hesaplamaya dahil etmek için yaptım

            for (int i = 0; i < egzersizListesi.Count; i++)
            {
                Egzersiz eg = (Egzersiz)egzersizListesi[i];
                if (eg.Agirlik > 0)  // Sadece ağırlığı olan egzersizler
                {
                    toplamAgirlik += eg.Agirlik;
                    adet++;
                }
            }

            if (adet == 0)
            {
                MessageBox.Show("Ağırlığı 0'dan büyük egzersiz bulunmadığı için ortalama hesaplanamaz!");
                return;
            }

            double ortalamaAgirlik = toplamAgirlik / adet;

            try
            {

                // Grafiğe ortalama değerini eklemek için ayrı bir  series nesnesi  eklemek için kullandım
                Series ortalamaSeri = chart1.Series.Add("Ortalama");    // chart üzerinde ortalamaları için yeni bi değer eklemek için ortalamaSeri adında nesnesi oluşturdum

                ortalamaSeri.ChartType = SeriesChartType.Line;      // chart ın çizgi mi sütun mu vb olcağını , rengini ve kalınlığını belirledim
                ortalamaSeri.Color = System.Drawing.Color.Red;
                ortalamaSeri.BorderWidth = 3;

                // X ekseninde egzersiz sayısı kadar nokta eklemediğimiz zaman line hata veriyor bu yüzden egzersiz sayısı kadar nokta ekledim
                for (int i = 0; i < egzersizListesi.Count; i++)
                {
                    Egzersiz eg = (Egzersiz)egzersizListesi[i];
                    ortalamaSeri.Points.AddXY(eg.Isim, ortalamaAgirlik);
                }


                MessageBox.Show($"Ağırlığı olan {adet} egzersizin ortalaması: {ortalamaAgirlik:F2} kg");




            }
            catch
            {
                MessageBox.Show("Ortalama zaten hesaplandı yeni değer ekleyip tekrar deneyiniz!");
            }


        }

        private void ProgramiGoster()
        {
            listBox2.Items.Clear();

            if (!File.Exists("seviyeler.txt"))
                return;

            StreamReader sr = new StreamReader("seviyeler.txt");
            string satir = sr.ReadLine();
            sr.Close();

            string[] parca = satir.Split(';');
            int seviye = Convert.ToInt32(parca[2]);

            if (seviye == 1)
            {
                listBox2.Items.Add("Pazartesi: Dinlenme");
                listBox2.Items.Add("Salı: Bench Press");
                listBox2.Items.Add("Çarşamba: Squat");
                listBox2.Items.Add("Perşembe: Dinlenme");
                listBox2.Items.Add("Cuma: Deadlift");
            }
            else if (seviye == 2)
            {
                listBox2.Items.Add("Pazartesi: Göğüs");
                listBox2.Items.Add("Salı: Sırt");
                listBox2.Items.Add("Çarşamba: Bacak");
                listBox2.Items.Add("Perşembe: Omuz");
                listBox2.Items.Add("Cuma: Kardiyo");
            }
            else
            {
                listBox2.Items.Add("Pazartesi: Koşu");
                listBox2.Items.Add("Salı: Bisiklet");
                listBox2.Items.Add("Çarşamba: Karın");
                listBox2.Items.Add("Perşembe: Yürüyüş");
                listBox2.Items.Add("Cuma: Kardiyo");
            }
        }

        // 3 form kullandığım için denediğim diğer close veya exit metodları işe yaramadığı yada çakışmaya sebebp olduğu için bu metoda başvurdum

        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.form.onformclosing?view=windowsdesktop-8.0 
        // bu metodun vs cod un içinde otomatik olduğunu ve visual olduğunu araştırıp kullandım sebebi form kapatıldığında uygulamanın tamamen kapanmasını sağlamak içindir normalde form kapatıldığında uygulama arka planda çalışmaya devam eder bu yüzden bunu kullanarak uygulamanın tamamen kapanmasını sağladım
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

    }
}