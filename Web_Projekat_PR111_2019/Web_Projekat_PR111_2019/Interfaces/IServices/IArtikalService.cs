using Web_Projekat_PR111_2019.DTO;

namespace Web_Projekat_PR111_2019.Interfaces.IServices
{
    public interface IArtikalService
    {
        Task DodajArtikal(DTODodajArtikal artikalDTO);

        Task<List<DTOArtikal>> DobaviSveArtikle();

        Task<DTOArtikal> DobaviArtikalpoID(int id);

        Task<DTOArtikal> AzurirajArtikal(int id, DTODodajArtikal artikalDTO);

        Task<DTOArtikal> ObrisiArtikal(int id);
    }
}
