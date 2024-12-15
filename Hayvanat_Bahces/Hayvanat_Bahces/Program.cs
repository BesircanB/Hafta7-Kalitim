using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hayvanat_Bahcesi
{




    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hayvan türünü seçiniz (1: Memeli, 2: Kuş): ");
            int secim = int.Parse(Console.ReadLine());

            Hayvan hayvan;

            if (secim == 1)
            {
                hayvan = new Memeli();
                Console.Write("Ad: ");
                hayvan.Ad = Console.ReadLine();
                Console.Write("Tür: ");
                hayvan.Tur = Console.ReadLine();
                Console.Write("Yaş: ");
                hayvan.Yas = int.Parse(Console.ReadLine());
                Console.Write("Tüy Rengi: ");
                ((Memeli)hayvan).TuyRengi = Console.ReadLine();
            }
            else if (secim == 2)
            {
                hayvan = new Kus();
                Console.Write("Ad: ");
                hayvan.Ad = Console.ReadLine();
                Console.Write("Tür: ");
                hayvan.Tur = Console.ReadLine();
                Console.Write("Yaş: ");
                hayvan.Yas = int.Parse(Console.ReadLine());
                Console.Write("Kanat Genişliği (cm): ");
                ((Kus)hayvan).KanatGenisligi = double.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
                return;
            }

            Console.WriteLine("\nHayvan Bilgileri:");
            hayvan.BilgiYazdir();
            Console.WriteLine("\nHayvanın Sesi:");
            hayvan.SesCikar();
            Console.ReadLine();
        }
    }
    class Hayvan
    {
        public string Ad { get; set; }
        public string Tur { get; set; }
        public int Yas { get; set; }

        public virtual void BilgiYazdir()
        {
            Console.WriteLine($"Ad: {Ad}");
            Console.WriteLine($"Tür: {Tur}");
            Console.WriteLine($"Yaş: {Yas}");
        }

        public virtual void SesCikar()
        {
            Console.WriteLine("Hayvan ses çıkarıyor...");
        }
    }

    // Memeli Sınıfı
    class Memeli : Hayvan
    {
        public string TuyRengi { get; set; }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Tüy Rengi: {TuyRengi}");
        }

        public override void SesCikar()
        {
            Console.WriteLine($"{Ad} isimli memeli hayvan kükredi!");
        }
    }

    // Kuş Sınıfı 
    class Kus : Hayvan
    {
        public double KanatGenisligi { get; set; }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Kanat Genişliği: {KanatGenisligi} cm");
        }

        public override void SesCikar()
        {
            Console.WriteLine($"{Ad} isimli kuş cik cik ötüyor!");
        }
    }


}
