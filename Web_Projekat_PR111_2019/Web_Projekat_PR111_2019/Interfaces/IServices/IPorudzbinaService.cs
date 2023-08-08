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
        Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int id);
        Task OtkaziPorudzbinu(int id);
        Task<List<Porudzbina>> DobaviSvePorudzbineKupca(int kupacId);
        Task<List<Porudzbina>> NovePorudzineProdavca(int id);
        Task<List<Porudzbina>> MojePorudzbine(int id);
        Task<List<Artikal>> DobaviArtiklePorudzbine(int idPorudzbine);
        Task<List<Artikal>> DobaviArtiklePorudzbineProdavca(int idPorudzbine);
    }
}
