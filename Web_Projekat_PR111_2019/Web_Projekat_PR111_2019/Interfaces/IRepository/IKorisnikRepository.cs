using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IRepository
{
    public interface IKorisnikRepository
    {
        Task<List<Korisnik>> DobaviKorisnike();
        Task<Korisnik> DobaviKorisnikaPoId(int id);
        Task<Korisnik> DobaviKorisnikaPoKorisnickomImenu(string korisnicko);
        Task<Korisnik> AzurirajKorisnika(Korisnik korisnik);
        Task<Korisnik> ObrisiKorisnika(int id);
        Task PotvrdiRegistraciju(int id);
        Task VerifikacijaProdavca(int id);
        Task OdbijVerifikaciju(int id);
        Task<List<Korisnik>> DobaviSveProdavce();
        Task<List<Korisnik>> DobaviKorisnikeKojiCekajuNaVerifikaciju();
    }
}
