using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Interfaces.IRepository
{
    public interface IRegistracijaRepository
    {
        Task Registracija(Korisnik kor);

        Task PotvrdiRegistraciju(int id);
    }
}
