using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Projekat_PR111_2019.Models
{
    public class Porudzbina
    {
        public int Id { get; set; }
        public Artikal Artikal { get; set; }
        public List<ArtikalIPorudzbina> ArtikliIPorudzbine { get; set; }
        public string Adresa { get; set; }
        public string KomentarPorudzbine { get; set; }
        public int Kolicina { get; set; }
        public float Cijena { get; set; }
        public string Email { get; set; }
        public PorudzbinaStatus StatusPorudzbine { get; set; }
        public DateTime VrijemeDostave { get; set; }
        public DateTime VrijemePOrudzbine { get; set; }
        public Kupac Kupac { get; set; }
        public int IDKupca { get; set; }
        public double Dostava { get; } = 100;
    }
}
