using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IRepository
{
    public interface IArtikalRepository
    {
        Task DodajArtikal(Artikal artikal);
        Task<Artikal> ObrisiArtikal(int id);
        Task<Artikal> AzurirajArtikal(Artikal artikal);
        Task<List<Artikal>> DobaviSveArtikle();

        Task<Artikal> DobaviArtikalPoId(int id);
    }
}
