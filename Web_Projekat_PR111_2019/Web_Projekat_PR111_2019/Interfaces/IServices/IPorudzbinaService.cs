using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IServices
{
    public interface IPorudzbinaService
    {
        Task<List<Porudzbina>> DobaviSvePorudzbine();
        Task<Porudzbina> DobaviPorudzbinuPoId(int id);
        Task<int> DodajPoruzbinu(DTODodajPorudzbinu porudzbinaDTO);
        Task<DTOPorudzbina> AzurirajPorudzbinu(int id, DTOPorudzbina porudzbinaDTO);
        Task<DTOPorudzbina> ObrisiPorudzbina(int id);
    }
}
