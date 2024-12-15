using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazaYonetim
{
    class Program
    {
        static void Main(string[] args)
        {
            // Ürün listesi oluşturma
            List<Urun> urunler = new List<Urun>();

            // Kitap ekleme
            Kitap kitap1 = new Kitap
            {
                Ad = "C# Programlama",
                Fiyat = 100,
                Yazar = "John Doe",
                ISBN = "123-456-789"
            };

            // Elektronik ürün ekleme
            Elektronik elektronik1 = new Elektronik
            {
                Ad = "Laptop",
                Fiyat = 15000,
                Marka = "TechBrand",
                Model = "Pro 2024"
            };

            // Ürünleri listeye ekleme
            urunler.Add(kitap1);
            urunler.Add(elektronik1);

            // Tüm ürünlerin bilgilerini yazdırma
            Console.WriteLine("=== Mağaza Ürün Listesi ===\n");
            foreach (var urun in urunler)
            {
                urun.BilgiYazdir();
            }
            Console.ReadLine();
        }
    }
    public abstract class Urun
    {
        public string Ad { get; set; }
        public double Fiyat { get; set; }

        // Soyut metot - her alt sınıf kendi implementasyonunu yapacak
        public abstract double HesaplaOdeme();

        // Genel bilgi yazdırma metodu
        public virtual void BilgiYazdir()
        {
            Console.WriteLine($"Ürün Adı: {Ad}");
            Console.WriteLine($"Fiyat: {Fiyat:C2}");
            Console.WriteLine($"Toplam Ödenecek Tutar: {HesaplaOdeme():C2}");
            Console.WriteLine("------------------------");
        }
    }

    // Kitap sınıfı
    public class Kitap : Urun
    {
        public string Yazar { get; set; }
        public string ISBN { get; set; }

        // %10 vergi ile ödeme hesaplama
        public override double HesaplaOdeme()
        {
            return Fiyat + (Fiyat * 0.10); // %10 vergi
        }

        public override void BilgiYazdir()
        {
            Console.WriteLine("--- Kitap Bilgileri ---");
            base.BilgiYazdir();
            Console.WriteLine($"Yazar: {Yazar}");
            Console.WriteLine($"ISBN: {ISBN}");
        }
    }

    // Elektronik sınıfı
    public class Elektronik : Urun
    {
        public string Marka { get; set; }
        public string Model { get; set; }

        // %25 vergi ile ödeme hesaplama
        public override double HesaplaOdeme()
        {
            return Fiyat + (Fiyat * 0.25); // %25 vergi
        }

        public override void BilgiYazdir()
        {
            Console.WriteLine("--- Elektronik Ürün Bilgileri ---");
            base.BilgiYazdir();
            Console.WriteLine($"Marka: {Marka}");
            Console.WriteLine($"Model: {Model}");
        }
    }
}
