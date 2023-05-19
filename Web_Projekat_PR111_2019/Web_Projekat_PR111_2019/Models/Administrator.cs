namespace Web_Projekat_PR111_2019.Models
{
    public class Administrator:Korisnik
    {
        public List<Kupac> Kupci { get; set; }
        public List<Prodavac> Prodavci { get; set; }
    }
}
