namespace Web_Projekat_PR111_2019.Models
{
    public class Prodavac:Korisnik
    {
        public List<Artikal> Artikli { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
        public Administrator Administrator { get; set; }
        public VerifikacijaStatus Verifikovan { get; set; }
        public PorudzbinaStatus StatusPoruzbine { get; set; }

    }
}
