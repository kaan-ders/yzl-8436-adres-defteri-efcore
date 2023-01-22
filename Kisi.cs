namespace AdresDefteri
{
    public abstract class Kisi //: IKisi
    {
        public int Id { get; set; }
        public string AdiSoyadi { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }

        public abstract bool IsValid();
    }

    public class Musteri : Kisi
    {
        public int SiparisSayisi { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(AdiSoyadi) == true || string.IsNullOrWhiteSpace(Adres) == true)
                return false;
            else
                return true;
        }
    }

    public class Personel : Kisi
    {
        public string Departman { get; set; }

        public override bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(AdiSoyadi) || string.IsNullOrWhiteSpace(Telefon) || string.IsNullOrWhiteSpace(Departman) || Telefon.Length > 11)
                return false;
            else
                return true;
        }
    }

    public class Paydas : Kisi
    {
        public string Sirket { get; set; }

        public override bool IsValid()
        {
            return string.IsNullOrWhiteSpace(AdiSoyadi) || string.IsNullOrWhiteSpace(Sirket) ? false : true;
        }
    }
}
