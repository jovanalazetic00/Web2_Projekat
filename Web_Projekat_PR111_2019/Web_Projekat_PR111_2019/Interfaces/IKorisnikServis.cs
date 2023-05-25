using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface IKorisnikServis
    {
        Task<DTOKorisnik> GetById(int id);
        Task<List<DTOKorisnik>> GetSviKorisnici();
        Task<List<DTOKorisnik>> GetSviProdavci();
        Task<DTOKorisnik> UpdateKorisnik(int id, DTOUpdateKorisnik DtoKorisnik);
        Task<DTOKorisnik> Registracija(DTORegistracija DtoReg);
        Task<DTOKorisnik> VerifikacijaPrihvacena(int id);
        Task<DTOKorisnik> VerifikacijaOdbijena(int id);
        
    }
}
