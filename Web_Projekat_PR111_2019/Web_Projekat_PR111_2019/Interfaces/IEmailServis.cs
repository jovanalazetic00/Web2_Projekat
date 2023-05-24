namespace Web_Projekat_PR111_2019.Interfaces
{
    public interface IEmailServis
    {
        Task PosaljiEmail(string email, string verifikacija);
    }
}
