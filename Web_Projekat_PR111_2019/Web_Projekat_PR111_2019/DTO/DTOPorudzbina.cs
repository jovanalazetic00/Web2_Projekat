using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOPorudzbina
    {
        public int Id { get; set; }
        public string Komentar { get; set; }
        public string Adresa { get; set; }
        public List<DTOArtikliIPorudzbine> ArtikliIPorudzbine { get; set; }
        public int IDKorisnika { get; set; }
        public DateTime VrijemePorudzbine { get; set; }
        public DateTime VrijemeIsporuke { get; set; }
        public int CijenaDostave { get; set; }
        public double Cijena { get; set; }
        public StatusPorudzbine StatusPorudzbine { get; set; }
        
    }
}
