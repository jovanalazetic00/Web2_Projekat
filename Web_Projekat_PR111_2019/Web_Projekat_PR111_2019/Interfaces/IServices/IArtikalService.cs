using Web_Projekat_PR111_2019.DTO;

namespace Web_Projekat_PR111_2019.Interfaces.IServices
{
    public interface IArtikalService
    {
        Task DodajArtikal(DTODodajArtikal artikalDTO);

        Task<List<DTOArtikal>> DobaviSveArtikle();

        Task<DTOArtikal> DobaviArtikalPoId(int id);

        Task<DTOArtikal> AzurirajArtikal(int id, DTODodajArtikal artikalDTO);

        Task<DTOArtikal> ObrisiArtikal(int id);
        Task<bool> DostupanArtikal(DTOArtikal artikalDTO);

        Task<List<DTOArtikal>> DobaviSveArtikleOdProdavca(int idProdavca);
    }
}
