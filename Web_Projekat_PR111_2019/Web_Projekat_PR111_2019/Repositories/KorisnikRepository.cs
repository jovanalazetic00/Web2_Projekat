
using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.DTO;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;
using static Raven.Client.Documents.Commands.MultiGet.GetRequest;

namespace Web_Projekat_PR111_2019.Repositories
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly DBContext DBC;
       
        public KorisnikRepository(DBContext dbCntx)
        {
            DBC = dbCntx;
        }
      
      

        public async Task<Korisnik> GetById(int id)
        {
            try
            {
                Korisnik korisnik = await DBC.Korisnici.Include(k => k.Porudzbine).FirstOrDefaultAsync(p => p.IdK == id);
                return korisnik;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Korisnik>> GetSviKorisnici()
        {
            try
            {
                List<Korisnik> korisnici = await DBC.Korisnici.Include(p => p.Porudzbine).ToListAsync();
                return korisnici;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<Korisnik>> GetSviProdavci()
        {
            try
            {
                List<Korisnik> prodavci = DBC.Korisnici.Include(a => a.Artikli).Where(tk => tk.TipKorisnika == TipKorisnika.Prodavac).ToList();
                return prodavci;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Korisnik> Registracija(Korisnik korisnik)
        {
            try
            {
                DBC.Korisnici.Add(korisnik);
                await DBC.SaveChangesAsync();
                return korisnik;
            }
            catch (Exception e)
            {
                return null;
                throw; 
            }
        }

        public async Task<Korisnik> UpdateKorisnik(Korisnik noviKorisnik)
        {
            Korisnik stariKorisnik = await DBC.Korisnici.FindAsync(noviKorisnik.IdK);

            if (stariKorisnik == null)
            {
                return null; // Korisnik nije pronađen
            }

            stariKorisnik.Ime = noviKorisnik.Ime;
            stariKorisnik.Email = noviKorisnik.Email;
            

            try
            {
                await DBC.SaveChangesAsync();
                return stariKorisnik;
            }
            catch (Exception e)
            {
                return null;
                throw; 
            }
        }

        public async  Task<Korisnik> VerifikacijaOdbijena(int id)
        {
            Korisnik korisnik = await DBC.Korisnici.FindAsync(id);

            if (korisnik == null)
            {
                return null; // Korisnik nije pronađen
            }

            korisnik.StatusKorisnika = StatusKorisnika.Odbijen;

            try
            {
                await DBC.SaveChangesAsync();
                return korisnik;
            }
            catch (Exception e)
            {
                return null;
                throw; 
            }
        }

        public async Task<Korisnik> VerifikacijaPrihvacena(int id)
        {
            Korisnik korisnik = await DBC.Korisnici.FindAsync(id);

            if (korisnik == null)
            {
                return null; // Korisnik nije pronađen
            }

            korisnik.StatusKorisnika = StatusKorisnika.Verifikovan;

            try
            {
                await DBC.SaveChangesAsync();
                return korisnik;
            }
            catch (Exception e)
            {
                return null;
                throw; 
            }
        }

        public async Task<Korisnik> GetKorisnikByEmail(string email)
        {
            return await DBC.Korisnici.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
