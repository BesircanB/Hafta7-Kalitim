using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calisan
{
    class Program
    {


        static void Main(string[] args)
        {

            Console.WriteLine("Çalışan türünü seçiniz (1: Yazılımcı, 2: Muhasebeci): ");
            int secim = int.Parse(Console.ReadLine());

            Calisan calisan;

            if (secim == 1)
            {
                calisan = new Yazilimci();
                Console.Write("Ad: ");
                calisan.Ad = Console.ReadLine();
                Console.Write("Soyad: ");
                calisan.Soyad = Console.ReadLine();
                Console.Write("Maaş: ");
                calisan.Maas = double.Parse(Console.ReadLine());
                calisan.Pozisyon = "Yazılımcı";
                Console.Write("Yazılım Dili: ");
                ((Yazilimci)calisan).YazilimDili = Console.ReadLine();
            }
            else if (secim == 2)
            {
                calisan = new Muhasebeci();
                Console.Write("Ad: ");
                calisan.Ad = Console.ReadLine();
                Console.Write("Soyad: ");
                calisan.Soyad = Console.ReadLine();
                Console.Write("Maaş: ");
                calisan.Maas = double.Parse(Console.ReadLine());
                calisan.Pozisyon = "Muhasebeci";
                Console.Write("Muhasebe Yazılımı: ");
                ((Muhasebeci)calisan).MuhasebeYazilimi = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
                return;
            }

            Console.WriteLine("\nÇalışan Bilgileri:");
            calisan.BilgiYazdir();
            Console.ReadLine();
        }
    }
    class Calisan
    {
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public double Maas { get; set; }
        public string Pozisyon { get; set; }

        public virtual void BilgiYazdir()
        {
            Console.WriteLine($"Ad: {Ad}");
            Console.WriteLine($"Soyad: {Soyad}");
            Console.WriteLine($"Maaş: {Maas} TL");
            Console.WriteLine($"Pozisyon: {Pozisyon}");
        }
    }

    // Yazilimci Sınıfı
    class Yazilimci : Calisan
    {
        public string YazilimDili { get; set; }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Yazılım Dili: {YazilimDili}");
        }
    }

    // Muhasebeci Sınıfı
    class Muhasebeci : Calisan
    {
        public string MuhasebeYazilimi { get; set; }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Muhasebe Yazılımı: {MuhasebeYazilimi}");
        }
    }


}
