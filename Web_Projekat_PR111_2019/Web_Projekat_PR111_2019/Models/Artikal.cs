namespace Web_Projekat_PR111_2019.Models
{
    public class Artikal
    {
        public string Id { get; set; }
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int KolicinaArtikla { get; set; }
        public string Opis { get; set; }
        public byte[] Slika { get; set; }
        public Prodavac Prodavac { get; set; }
        public string IDProdavca { get; set; }
        public List<ArtikalIPorudzbina> ArtikliIporudzbine { get; set; }

    }
}
