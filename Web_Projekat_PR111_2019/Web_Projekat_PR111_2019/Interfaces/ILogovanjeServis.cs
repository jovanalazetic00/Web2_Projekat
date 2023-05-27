using Web_Projekat_PR111_2019.DTO;

namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface ILogovanjeServis
    {
        Task<string> LogovanjeKorisnika(DTOLogovanje logovanjeDto);
    }
}
