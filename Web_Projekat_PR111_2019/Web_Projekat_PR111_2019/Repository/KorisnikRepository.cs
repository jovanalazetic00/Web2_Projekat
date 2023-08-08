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

        public async Task<Korisnik> DobaviKorisnikaPoId(int id)
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

        public async Task<List<Korisnik>> DobaviKorisnikeKojiCekajuNaVerifikaciju()
        {
            List<Korisnik> neverifikovani = new List<Korisnik>();

            var korisnici = await DobaviKorisnike();

            foreach (var k in korisnici)
            {
                if (k.Verifikovan == false && k.StatusVerifrikacije == StatusVerifikacije.UObradi)
                {
                    neverifikovani.Add(k);
                }
            }
            return neverifikovani;
        }

        public async Task<List<Korisnik>> DobaviSveProdavce()
        {
            List<Korisnik> prodavci = new List<Korisnik>();

            var korisnici = await dbContext.Korisnici.ToListAsync();

            foreach (var korisnik in korisnici)
            {
                if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
                {
                    prodavci.Add(korisnik);
                }
            }

            return prodavci;
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

        public async Task OdbijVerifikaciju(int id)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(u => u.IdKorisnika == id);

            if (korisnik != null)
            {
                if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
                {
                    korisnik.Verifikovan = false;
                    korisnik.StatusVerifrikacije = StatusVerifikacije.Odbijen;

                    dbContext.Update(korisnik);
                    await dbContext.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("Korisnik ne postoji");
            }
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

        public async Task VerifikacijaProdavca(int id)
        {
            var korisnik = dbContext.Korisnici.FirstOrDefault(k => k.IdKorisnika == id);


            if (korisnik == null)
            {
                throw new Exception("Prodavac nije pronađen.");
            }


            if (korisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                if (korisnik.Verifikovan == false)
                {
                    korisnik.Verifikovan = true;
                    korisnik.StatusVerifrikacije = StatusVerifikacije.Verifikovan;
                }
                else
                {
                    throw new Exception("Prodavac je vec verifikovan.");
                }
            }



            dbContext.Update(korisnik);
            await dbContext.SaveChangesAsync();
        }
    }
}
