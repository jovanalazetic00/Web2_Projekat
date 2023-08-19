namespace Web_Projekat_PR111_2019.DTO
{
    public class DTODodajPorudzbinu
    {
        public List<DTODodajArtikalIPorudzbina> ArtikliIPorudzbine { get; set; } = null;
        public string AdresaIsporuke { get; set; }
        public string KomentarPorudzbine { get; set; }
        
    }
}
