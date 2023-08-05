namespace Web_Projekat_PR111_2019.DTO
{
    public class DTODodajPorudzbinu
    {
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public List<DTODodajArtikalIPorudzbina> Stavke { get; set; } = null;
    }
}
