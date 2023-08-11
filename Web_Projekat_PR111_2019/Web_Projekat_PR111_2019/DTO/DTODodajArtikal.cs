namespace Web_Projekat_PR111_2019.DTO
{
    public class DTODodajArtikal
    {
        public string Naziv { get; set; }
        public double Cijena { get; set; }
        public int KolicinaArtikla { get; set; }
        public string Opis { get; set; }
        public IFormFile? Slika { get; set; }
    }
}
