using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface IKorisnikRepository
    {
       
        Task<List<Korisnik>> GetSviKorisnici();
        Task<List<Korisnik>> GetSviProdavci();
        Task<Korisnik> GetById(int id);
        Task<Korisnik> UpdateKorisnik(Korisnik korisnik);
        Task<Korisnik> VerifikacijaPrihvacena(int id);
        Task<Korisnik> VerifikacijaOdbijena(int id);
        Task<Korisnik> Registracija(Korisnik korisnik);
    }
}
