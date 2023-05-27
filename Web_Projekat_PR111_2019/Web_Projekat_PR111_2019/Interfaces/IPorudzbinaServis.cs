using Web_Projekat_PR111_2019.DTO;

namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface IPorudzbinaServis
    {
        Task<List<DTOPorudzbina>> GetSvePorudzbine();
        Task<List<DTOPorudzbina>> GetSveIsporucenePorudzbine(int id);
        Task<List<DTOPorudzbina>> GetSvePorudzbineUObradi(int id);
        Task<DTOPorudzbina> GetByIdPorudzbina(int id);
        Task<DTOPorudzbina> KreiranjePOrudzbine(int userId, DTOKreiranjePorudzbine dtoPorudzbina);
        Task<bool> OdbijanjePorudzbine(int id);
    }
}
