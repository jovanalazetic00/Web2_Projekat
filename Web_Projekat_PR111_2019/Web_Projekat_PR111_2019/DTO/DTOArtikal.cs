namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOArtikal
    {
        public int ArtikalId { get; set; }
        public string Naziv { get; set; }
        public int Cijena { get; set; }
        public int KolicinaArtikla { get; set; }
        public string Opis { get; set; }
        public byte[]? Slika { get; set; }
    }
}
