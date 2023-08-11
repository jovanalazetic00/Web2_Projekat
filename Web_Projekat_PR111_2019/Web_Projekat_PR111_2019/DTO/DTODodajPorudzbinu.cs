namespace Web_Projekat_PR111_2019.DTO
{
    public class DTODodajPorudzbinu
    {
        public string AdresaIsporuke { get; set; }
        public string KomentarPorudzbine { get; set; }
        public List<DTODodajArtikalIPorudzbina> Stavke { get; set; } = null;
    }
}
