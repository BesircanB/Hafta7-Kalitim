using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaYonetimSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hesap türünü seçiniz (1: Vadesiz Hesap, 2: Vadeli Hesap): ");
            int secim = int.Parse(Console.ReadLine());

            Hesap hesap;

            if (secim == 1)
            {
                hesap = new VadesizHesap();
                Console.Write("Hesap No: ");
                hesap.HesapNo = Console.ReadLine();
                Console.Write("Hesap Sahibi: ");
                hesap.HesapSahibi = Console.ReadLine();
                Console.Write("Başlangıç Bakiyesi: ");
                hesap.Bakiye = double.Parse(Console.ReadLine());
                Console.Write("Ek Hesap Limiti: ");
                ((VadesizHesap)hesap).EkHesapLimiti = double.Parse(Console.ReadLine());
            }
            else if (secim == 2)
            {
                hesap = new VadeliHesap();
                Console.Write("Hesap No: ");
                hesap.HesapNo = Console.ReadLine();
                Console.Write("Hesap Sahibi: ");
                hesap.HesapSahibi = Console.ReadLine();
                Console.Write("Başlangıç Bakiyesi: ");
                hesap.Bakiye = double.Parse(Console.ReadLine());
                Console.Write("Vade Süresi (ay): ");
                ((VadeliHesap)hesap).VadeSuresi = int.Parse(Console.ReadLine());
                Console.Write("Faiz Oranı (%): ");
                ((VadeliHesap)hesap).FaizOrani = double.Parse(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Geçersiz seçim!");
                return;
            }

            while (true)
            {
                Console.WriteLine("\nİşlem Seçiniz:");
                Console.WriteLine("1: Para Yatır");
                Console.WriteLine("2: Para Çek");
                Console.WriteLine("3: Hesap Bilgilerini Göster");
                Console.WriteLine("4: Çıkış");

                int islem = int.Parse(Console.ReadLine());

                switch (islem)
                {
                    case 1:
                        Console.Write("Yatırılacak miktar: ");
                        double yatirilanMiktar = double.Parse(Console.ReadLine());
                        hesap.ParaYatir(yatirilanMiktar);
                        break;

                    case 2:
                        Console.Write("Çekilecek miktar: ");
                        double cekilenMiktar = double.Parse(Console.ReadLine());
                        hesap.ParaCek(cekilenMiktar);
                        break;

                    case 3:
                        hesap.BilgiYazdir();
                        break;

                    case 4:
                        return;

                    default:
                        Console.WriteLine("Geçersiz işlem!");
                        break;
                }
            }
            Console.ReadLine();

        }
    }
    class Hesap
    {
        public string HesapNo { get; set; }
        public double Bakiye { get; set; }
        public string HesapSahibi { get; set; }

        public virtual void ParaYatir(double miktar)
        {
            if (miktar > 0)
            {
                Bakiye += miktar;
                Console.WriteLine($"{miktar} TL yatırıldı. Yeni bakiye: {Bakiye} TL");
            }
            else
            {
                Console.WriteLine("Geçersiz miktar!");
            }
        }

        public virtual bool ParaCek(double miktar)
        {
            if (miktar > 0 && miktar <= Bakiye)
            {
                Bakiye -= miktar;
                Console.WriteLine($"{miktar} TL çekildi. Yeni bakiye: {Bakiye} TL");
                return true;
            }
            Console.WriteLine("Yetersiz bakiye veya geçersiz miktar!");
            return false;
        }

        public virtual void BilgiYazdir()
        {
            Console.WriteLine($"Hesap No: {HesapNo}");
            Console.WriteLine($"Hesap Sahibi: {HesapSahibi}");
            Console.WriteLine($"Bakiye: {Bakiye} TL");
        }
    }

    class VadesizHesap : Hesap
    {
        public double EkHesapLimiti { get; set; }

        public override bool ParaCek(double miktar)
        {
            if (miktar > 0)
            {
                if (miktar <= Bakiye + EkHesapLimiti)
                {
                    if (miktar <= Bakiye)
                    {
                        Bakiye -= miktar;
                    }
                    else
                    {
                        double ekHesapKullanimi = miktar - Bakiye;
                        EkHesapLimiti -= ekHesapKullanimi;
                        Bakiye = 0;
                    }
                    Console.WriteLine($"{miktar} TL çekildi. Yeni bakiye: {Bakiye} TL");
                    Console.WriteLine($"Kalan ek hesap limiti: {EkHesapLimiti} TL");
                    return true;
                }
            }
            Console.WriteLine("Yetersiz bakiye ve limit veya geçersiz miktar!");
            return false;
        }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Ek Hesap Limiti: {EkHesapLimiti} TL");
        }
    }

    class VadeliHesap : Hesap
    {
        public int VadeSuresi { get; set; }  // Ay cinsinden
        public double FaizOrani { get; set; }
        private DateTime VadeBaslangicTarihi;

        public VadeliHesap()
        {
            VadeBaslangicTarihi = DateTime.Now;
        }

        public override bool ParaCek(double miktar)
        {
            TimeSpan gecenSure = DateTime.Now - VadeBaslangicTarihi;
            if (gecenSure.TotalDays < VadeSuresi * 30)
            {
                Console.WriteLine($"Vade süresi dolmadan para çekemezsiniz! Kalan süre: {VadeSuresi * 30 - gecenSure.TotalDays:F0} gün");
                return false;
            }
            return base.ParaCek(miktar);
        }

        public override void ParaYatir(double miktar)
        {
            if (miktar > 0)
            {
                double faizliMiktar = miktar * (1 + (FaizOrani / 100));
                Bakiye += faizliMiktar;
                Console.WriteLine($"{miktar} TL yatırıldı. Faizli bakiye: {Bakiye} TL");
            }
            else
            {
                Console.WriteLine("Geçersiz miktar!");
            }
        }

        public override void BilgiYazdir()
        {
            base.BilgiYazdir();
            Console.WriteLine($"Vade Süresi: {VadeSuresi} ay");
            Console.WriteLine($"Faiz Oranı: %{FaizOrani}");
        }
    }
}
