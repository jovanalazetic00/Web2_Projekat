using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Repository
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly DBContext dbContext;

        public KorisnikRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Korisnik> AzurirajKorisnika(Korisnik korisnik)
        {
            dbContext.Update(korisnik);
            await dbContext.SaveChangesAsync();
            return korisnik;
        }

        public async Task<Korisnik> DobaviKorisnikaPoID(int id)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.IdKorisnika == id);

            if (korisnik == null)
            {
                return null;
            }
            return korisnik;
        }

        public async Task<Korisnik> DobaviKorisnikaPoKorisnickomImenu(string korisnicko)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.KorisnickoIme == korisnicko);

            if (korisnik == null)
            {
                return null;
            }
            return korisnik;
        }

        public async Task<List<Korisnik>> DobaviKorisnike()
        {
            return await dbContext.Korisnici.ToListAsync();
        }

        public async Task<Korisnik> ObrisiKorisnika(int id)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.IdKorisnika == id);
            if (korisnik != null)
            {
                dbContext.Remove(korisnik);
                await dbContext.SaveChangesAsync();
            }
            return korisnik;
        }

        public async Task PotvrdiRegistraciju(int id)
        {
            var korisnik = await dbContext.Korisnici.FindAsync(id);
            if (korisnik == null)
            {
                throw new Exception("Korisnik ne postoji.");
            }

            if (korisnik.PotvrdaRegistracije == true)
            {
                throw new Exception("Korisnikova registracija je vec potvrdjena!");
            }


            korisnik.PotvrdaRegistracije = true;


            dbContext.Update(korisnik);
            await dbContext.SaveChangesAsync();
        }
    }
}
