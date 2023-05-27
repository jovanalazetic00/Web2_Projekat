using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface IPorudzbinaRepository
    {
        Task<Porudzbina> KreiranjePorudzbine(Porudzbina porudzbina);
        Task CuvanjePromjena();
        Task<List<Porudzbina>> GetSvePorudzbine();
        Task<Porudzbina> GetPorudzbinaById(int id);
        
    }
}
