using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOPorudzbina
    {
        public int IdPorudzbine { get; set; }
        public List<DTOArtikliIPorudzbine> ArtikliIPorudzbine { get; set; }
        public string AdresaIsporuke { get; set; }
        public string KomentarPorudzbine { get; set; }
        
        
       
        
    }
}
