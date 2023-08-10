using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.DTO
{
    public class DTOFormaRegistracije
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string PotvrdaLozinke { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public IFormFile? Slika { get; set; }
    }
}
