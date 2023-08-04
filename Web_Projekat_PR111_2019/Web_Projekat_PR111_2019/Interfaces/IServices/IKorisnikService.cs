using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IServices
{
    public interface IKorisnikService
    {
        Task<List<DTOKorisnik>> DobaviKorisnike();
        Task<Korisnik> DobaviKorisnikapoID(int id);
        Task<DTOKorisnik> AzurirajKorisnika(int id, DTOAzuriranjeKorisnika korisnikDto);
        Task<DTOKorisnik> ObrisiKorisnika(int id);
        Task PotvrdiRegistraciju(int id);
        Task OdbijRegistraciju(int id);

    }
}
