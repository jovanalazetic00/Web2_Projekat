using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IServices
{
    public interface IRegistracijaService
    {
        Task<DTOKorisnik> Registracija(DTOFormaRegistracije registracijaDTO);

        Task PotvrdiRegistraciju(int id);
        Task<string> LogIn(DTOLogIn logovanjeDTO);
    }
}
