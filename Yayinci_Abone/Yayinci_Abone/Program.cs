using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yayinci_Abone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Yayıncı oluşturma
            HaberYayincisi haberAjansi = new HaberYayincisi("Haber Ajansı");

            // Aboneler oluşturma
            Kullanici kullanici1 = new Kullanici("Ali");
            Kullanici kullanici2 = new Kullanici("Ayşe");
            Kullanici kullanici3 = new Kullanici("Mehmet");

            // Aboneleri ekleme
            haberAjansi.AboneEkle(kullanici1);
            haberAjansi.AboneEkle(kullanici2);
            haberAjansi.AboneEkle(kullanici3);

            // Mevcut aboneleri listeleme
            haberAjansi.AboneleriListele();

            // Haber gönderme
            haberAjansi.HaberGonder("Yeni teknoloji haberleri yayında!");

            // Bir aboneyi çıkarma
            haberAjansi.AboneCikar(kullanici2);

            // Güncel abone listesi
            haberAjansi.AboneleriListele();

            // Yeni bir haber daha gönderme
            haberAjansi.HaberGonder("Son dakika: Önemli gelişme!");

            Console.ReadLine();
        }
    }

    public interface IAbone
    {
        void BilgiAl(string mesaj);
        string AboneAdi { get; }
    }

    // Yayıncı arayüzü
    public interface IYayinci
    {
        void AboneEkle(IAbone abone);
        void AboneCikar(IAbone abone);
        void AboneleriListele();
        void HaberGonder(string mesaj);
    }

    // Yayıncı sınıfı
    public class HaberYayincisi : IYayinci
    {
        private List<IAbone> aboneler;
        private string yayinciAdi;

        public HaberYayincisi(string ad)
        {
            aboneler = new List<IAbone>();
            yayinciAdi = ad;
        }

        public void AboneEkle(IAbone abone)
        {
            aboneler.Add(abone);
            Console.WriteLine($"\n{abone.AboneAdi} adlı kullanıcı {yayinciAdi}'na abone oldu.");
        }

        public void AboneCikar(IAbone abone)
        {
            aboneler.Remove(abone);
            Console.WriteLine($"\n{abone.AboneAdi} adlı kullanıcı {yayinciAdi}'ndan ayrıldı.");
        }

        public void AboneleriListele()
        {
            Console.WriteLine($"\n{yayinciAdi} - Mevcut Aboneler:");
            foreach (var abone in aboneler)
            {
                Console.WriteLine($"- {abone.AboneAdi}");
            }
        }

        public void HaberGonder(string mesaj)
        {
            Console.WriteLine($"\n{yayinciAdi} yeni bir haber yayınladı!");

            foreach (var abone in aboneler)
            {
                abone.BilgiAl($"{yayinciAdi}'dan Haber: {mesaj}");
            }
        }
    }

    // Abone sınıfı
    public class Kullanici : IAbone
    {
        private string ad;
        public string AboneAdi => ad;

        public Kullanici(string ad)
        {
            this.ad = ad;
        }

        public void BilgiAl(string mesaj)
        {
            Console.WriteLine($"{ad} için bildirim: {mesaj}");
        }
    }

}
