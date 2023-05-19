using System.ComponentModel.DataAnnotations;

namespace Web_Projekat_PR111_2019.Models
{
    public class Korisnik
    {
        public string KorisnickoIme { get; set; }
        
        public string Email { get; set; }
        
        public string Lozinka { get; set; }
        
        public string ImePrezime { get; set; }
       
        public DateTime DatumRodjenja { get; set; }
       
        public string Adresa { get; set; }
        public string Slika { get; set; }
        
    }
}

