using System.ComponentModel.DataAnnotations;

namespace Web_Projekat_PR111_2019.Models
{
    public class Korisnik
    {
        public int IdK { get; set; }
        public string KorisnickoIme { get; set; }

        public string Email { get; set; }

        public string Lozinka { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }

        public DateTime DatumRodjenja { get; set; }

        public string Adresa { get; set; }
        public byte[] Slika { get; set; }
        public TipKorisnika TipKorisnika {get; set; }
        public StatusKorisnika StatusKorisnika { get; set; }
        public List<Artikal> Artikli { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
       
        
    }
}

