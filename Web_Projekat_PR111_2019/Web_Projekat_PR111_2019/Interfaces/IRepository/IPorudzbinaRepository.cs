using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IRepository
{
    public interface IPorudzbinaRepository
    {
        Task<List<Porudzbina>> DobaviSvePorudzbine();
        Task<Porudzbina> DobaviPorudzbinuPoId(int id);
        Task DodajPoruzbinu(Porudzbina porudzbina);
        Task<Porudzbina> AzurirajPorudzbinu(Porudzbina porudzbina);
        Task<Porudzbina> ObrisiPorudzbinu(int id);
        Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int id);
        Task OtkazivanjePorudzbinu(int id);
        Task<List<Porudzbina>> NovePorudzineOdProdavca(int id);
        Task<List<Porudzbina>> SvePorudzbineOdProdavca(int id);
        Task<List<Porudzbina>> MojePorudzbine(int id);
        Task<List<Artikal>> DobaviArtikleOdPorudzbine(int idPorudzbine);
        Task<List<Artikal>> DobaviArtiklePorudzbineProdavca(int idPorudzbine, int idProdavca);
    }
}
