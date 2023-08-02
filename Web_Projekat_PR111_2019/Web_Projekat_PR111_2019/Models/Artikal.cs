namespace Web_Projekat_PR111_2019.Models
{
    public class Artikal
    {
        public int ArtikalId { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int KolicinaArtikla { get; set; }
        public string Opis { get; set; }
        public byte[]? Slika { get; set; }
        public List<ArtikalIPorudzbina> ArtikliIPorudzbine { get; set; }
        public bool Obrisan { get; set; }
        public Korisnik Korisnik { get; set; }
        public int IdKorisnika { get; set; }

        public Artikal()
        {
            ArtikliIPorudzbine = new List<ArtikalIPorudzbina>();
        }
    }
}
