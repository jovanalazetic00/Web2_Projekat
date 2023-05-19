namespace Web_Projekat_PR111_2019.Models
{
    public class Kupac:Korisnik
    {
        public List<Porudzbina> Porudzbine { get; set; }
        public PorudzbinaStatus StatusPorudzbine { get; set; }
        public Administrator Administrator { get; set; }
    }
}
