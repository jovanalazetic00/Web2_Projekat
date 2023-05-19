namespace Web_Projekat_PR111_2019.Models
{
    public class ArtikalIPorudzbina
    {
        public Porudzbina Porudzbina { get; set; }
        public Artikal Artikal { get; set; }
        public string IDPorudzbine { get; set; }
        public string IDArtikla { get; set; }
        public int KolicinaArtikla { get; set; }
        public int CijenaPorudzbine { get; set; }
    }
}
