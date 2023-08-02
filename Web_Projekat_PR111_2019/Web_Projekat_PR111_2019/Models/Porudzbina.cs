using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Projekat_PR111_2019.Models
{
    public class Porudzbina
    {
        public int IdPorudzbine { get; set; } 
        public List<ArtikalIPorudzbina> ArtikliIPorudzbine { get; set; }
        public string AdresaIsporuke { get; set; }
        public string KomentarPorudzbine { get; set; }
        
        public double CijenaPorudzbine { get; set; }
        public StatusPorudzbine StatusPorudzbine { get; set; }
        public DateTime VrijemeIsporuke { get; set; }
        public DateTime VrijemePorudzbine { get; set; }
       
        public Korisnik Korisnik { get; set; }
        public int IdKorisnika { get; set; }
        public bool Obrisan { get; set; }

        public Porudzbina()
        {
            ArtikliIPorudzbine = new List<ArtikalIPorudzbina>();
        }
    }
}
