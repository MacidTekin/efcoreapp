using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }

        public int OgrenciId { get; set; }

        public Ogrenci Ogrenci { get; set; } = null!;

        public int KursId { get; set; } //hangi kurstan bu bilgiyi aldığımız söyleyeceğiz 1 numaralı kayıt => 5 numralı öğrenci => 8 numaralı kursa gibi

        public Kurs Kurs { get; set; } = null!;

        public DateTime KayitTarihi { get; set; }

    }
}