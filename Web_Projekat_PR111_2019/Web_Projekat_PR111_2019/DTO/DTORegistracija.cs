using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.DTO
{
    public class DTORegistracija
    {
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string RepeatLozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public string Slika { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
    }
}
