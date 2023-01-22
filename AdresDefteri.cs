using System.Collections;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace AdresDefteri
{
    public interface IAdresDefteri
    {
        bool Ekle(Kisi kisi);
        void Sil(string aranacak);
        void Arama(string aranacak);
        void TumunuGoruntule();
    }

    public class AdresDefteri : IAdresDefteri
    {
        public List<Kisi> Liste { get; set; }

        public AdresDefteri()
        {
            Liste = new List<Kisi>();
            
            using(var context = new AdresDefteriDbContext())
            {
                var musteriler = context.Musteri.ToList();
                var paydaslar = context.Paydas.ToList();
                var personeller = context.Personel.ToList();

                Liste.AddRange(musteriler);
                Liste.AddRange(paydaslar);
                Liste.AddRange(personeller);
            }
        }

        public bool Ekle(Kisi kisi)
        {
            if (kisi.IsValid())
            {
                Liste.Add(kisi);
                
                using(var context = new AdresDefteriDbContext())
                {
                    context.Add(kisi);
                    context.SaveChanges();
                }

                return true;
            }

            return false;
        }

        public void Sil(string aranacak)
        {
            for (int i = 0; i < Liste.Count; i++)
            {
                if (Liste[i].AdiSoyadi.ToLower() == aranacak.ToLower())
                    Liste.RemoveAt(i);
            }

            //dbden sil
            using(var context = new AdresDefteriDbContext())
            {
                var silinecekPersoneller = context.Personel.Where(x=> x.AdiSoyadi.ToLower() == aranacak.ToLower()).ToList();
                var silinecekMusteriler = context.Musteri.Where(x=> x.AdiSoyadi.ToLower() == aranacak.ToLower()).ToList();
                var silinecekPaydaslar = context.Paydas.Where(x=> x.AdiSoyadi.ToLower() == aranacak.ToLower()).ToList();

                context.RemoveRange(silinecekPersoneller);
                context.RemoveRange(silinecekMusteriler);
                context.RemoveRange(silinecekPaydaslar);

                context.SaveChanges();
            }
        }

        public void Arama(string aranacak)
        {
            foreach (Kisi kisi in Liste)
            {
                if(kisi.AdiSoyadi.ToLower().Contains(aranacak.ToLower()))
                {
                    Console.WriteLine("Adı Soyadı: " + kisi.AdiSoyadi);
                    Console.WriteLine("Telefon: " + kisi.Telefon);
                    Console.WriteLine("Adres: " + kisi.Adres);
                    Console.WriteLine("-------------------------------");
                    Console.WriteLine("");
                }
            }
        }

        public void TumunuGoruntule()
        {
            foreach (Kisi kisi in Liste)
            {
                Console.WriteLine("Adı Soyadı: " + kisi.AdiSoyadi);
                Console.WriteLine("Telefon: " + kisi.Telefon);
                Console.WriteLine("Adres: " + kisi.Adres);
                Console.WriteLine("-------------------------------");
                Console.WriteLine("");
            }
        }
    }
}