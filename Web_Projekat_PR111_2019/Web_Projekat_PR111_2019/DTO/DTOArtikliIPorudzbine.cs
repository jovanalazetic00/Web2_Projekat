namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOArtikliIPorudzbine
    {
        public int IdArtikalIPorudzbina { get; set; }
        public int KolicinaArtikla { get; set; }
        public DTOArtikal Artikal { get; set; } 
    }
}
