namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOKreiranjePorudzbine
    {
        public List<DTOKreiranjeArtikliIPorudzbine> ArtikliIPorudzbine { get; set; } = null!;
        public string? Komentar { get; set; }
        public string Adresa { get; set; } = null!;
        
    }
}
