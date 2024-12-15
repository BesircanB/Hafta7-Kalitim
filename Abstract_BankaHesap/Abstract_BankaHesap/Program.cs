using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstract_BankaHesap
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Birikim Hesabı Test
                Console.WriteLine("=== Birikim Hesabı Test ===");
                var birikimHesabi = new BirikimHesabi("BH001", 1000, 5);
                birikimHesabi.ParaYatir(500);  // 500 + (%5 faiz) = 525
                birikimHesabi.ParaCek(200);
                Console.WriteLine(birikimHesabi.HesapOzeti());
                foreach (var islem in birikimHesabi.IslemGecmisi)
                {
                    Console.WriteLine(islem);
                }

                // Vadesiz Hesap Test
                Console.WriteLine("\n=== Vadesiz Hesap Test ===");
                var vadesizHesap = new VadesizHesap("VH001", 1000);
                vadesizHesap.ParaYatir(300);
                vadesizHesap.ParaCek(500);  // +1.5TL işlem ücreti
                Console.WriteLine(vadesizHesap.HesapOzeti());
                foreach (var islem in vadesizHesap.IslemGecmisi)
                {
                    Console.WriteLine(islem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
            Console.ReadLine();
        }
    }

    public interface IBankaHesabi
    {
        DateTime HesapAcilisTarihi { get; set; }
        string HesapOzeti();
    }

    // Soyut Hesap sınıfı
    public abstract class Hesap : IBankaHesabi
    {
        public string HesapNo { get; set; }
        public decimal Bakiye { get; protected set; }
        public DateTime HesapAcilisTarihi { get; set; }
        public List<string> IslemGecmisi { get; private set; }

        protected Hesap(string hesapNo, decimal baslangicBakiye)
        {
            HesapNo = hesapNo;
            Bakiye = baslangicBakiye;
            HesapAcilisTarihi = DateTime.Now;
            IslemGecmisi = new List<string>();
        }

        public virtual void ParaYatir(decimal miktar)
        {
            if (miktar <= 0)
                throw new ArgumentException("Para yatırma miktarı 0'dan büyük olmalıdır.");

            Bakiye += miktar;
            IslemKaydet($"Para Yatırma: +{miktar:C}");
        }

        public virtual void ParaCek(decimal miktar)
        {
            if (miktar <= 0)
                throw new ArgumentException("Para çekme miktarı 0'dan büyük olmalıdır.");

            if (miktar > Bakiye)
                throw new InvalidOperationException("Yetersiz bakiye.");

            Bakiye -= miktar;
            IslemKaydet($"Para Çekme: -{miktar:C}");
        }

        protected void IslemKaydet(string islem)
        {
            IslemGecmisi.Add($"{DateTime.Now}: {islem} - Bakiye: {Bakiye:C}");
        }

        public virtual string HesapOzeti()
        {
            return $"\nHesap No: {HesapNo}" +
                   $"\nHesap Türü: {this.GetType().Name}" +
                   $"\nAçılış Tarihi: {HesapAcilisTarihi}" +
                   $"\nGüncel Bakiye: {Bakiye:C}" +
                   $"\n\nSon İşlemler:";
        }
    }

    // BirikimHesabi sınıfı
    public class BirikimHesabi : Hesap
    {
        public decimal FaizOrani { get; private set; }

        public BirikimHesabi(string hesapNo, decimal baslangicBakiye, decimal faizOrani)
            : base(hesapNo, baslangicBakiye)
        {
            FaizOrani = faizOrani;
        }

        public override void ParaYatir(decimal miktar)
        {
            decimal faizMiktari = miktar * (FaizOrani / 100);
            base.ParaYatir(miktar + faizMiktari);
            IslemKaydet($"Faiz Kazancı: +{faizMiktari:C}");
        }

        public override string HesapOzeti()
        {
            return base.HesapOzeti() + $"\nFaiz Oranı: %{FaizOrani}";
        }
    }

    // VadesizHesap sınıfı
    public class VadesizHesap : Hesap
    {
        private const decimal IslemUcreti = 1.5m;

        public VadesizHesap(string hesapNo, decimal baslangicBakiye)
            : base(hesapNo, baslangicBakiye)
        {
        }

        public override void ParaCek(decimal miktar)
        {
            if (miktar + IslemUcreti > Bakiye)
                throw new InvalidOperationException("Yetersiz bakiye (işlem ücreti dahil).");

            base.ParaCek(miktar);
            Bakiye -= IslemUcreti;
            IslemKaydet($"İşlem Ücreti: -{IslemUcreti:C}");
        }

        public override string HesapOzeti()
        {
            return base.HesapOzeti() + $"\nİşlem Ücreti: {IslemUcreti:C}";
        }
    }
}
