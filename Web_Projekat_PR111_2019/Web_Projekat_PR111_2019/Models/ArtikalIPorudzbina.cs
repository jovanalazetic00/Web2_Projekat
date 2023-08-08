namespace Web_Projekat_PR111_2019.Models
{
    public class ArtikalIPorudzbina
    {
        public Porudzbina Porudzbina { get; set; }
        public Artikal Artikal { get; set; }
        public int IDPorudzbineAIP { get; set; }
        public int IDArtiklaAIP { get; set; }
        public int KolicinaArtikla { get; set; }
       
    }
}
