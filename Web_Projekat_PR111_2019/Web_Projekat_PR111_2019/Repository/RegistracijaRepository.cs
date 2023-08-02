using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Repository
{
    public class RegistracijaRepository : IRegistracijaRepository
    {
        private readonly DBContext dbContext;

        public RegistracijaRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task PotvrdiRegistraciju(int id)
        {
            Korisnik korisnik = dbContext.Korisnici.FirstOrDefault(k => k.IdKorisnika == id);


            if (korisnik == null)
            {
                throw new Exception("Korisnik nije pronađen.");
            }

            if (korisnik.PotvrdaRegistracije)
            {
                throw new Exception("Registracija je već potvrđena.");
            }


            korisnik.PotvrdaRegistracije = true;
            dbContext.Update(korisnik);
            await dbContext.SaveChangesAsync();
        }

        public async Task Registracija(Korisnik kor)
        {
            var korisnik = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.Email == kor.Email);
            var tipKorisnika = kor.TipKorisnika;

            if (korisnik != null)
            {
                throw new Exception("Email već postoji u bazi");
            }


            var korisnik1 = await dbContext.Korisnici.FirstOrDefaultAsync(k => k.KorisnickoIme == kor.KorisnickoIme);

            if (korisnik1 != null)
            {
                throw new Exception("Korisnik sa datim korisnickim imenom već postoji u bazi");
            }


            if (tipKorisnika == TipKorisnika.Administrator)
            {
                kor.PotvrdaRegistracije = true;
                kor.Verifikovan = true;
                kor.StatusVerifrikacije = StatusVerifikacije.Verifikovan;
            }
            if (tipKorisnika == TipKorisnika.Kupac)
            {
                kor.Verifikovan = true;
                kor.StatusVerifrikacije = StatusVerifikacije.Verifikovan;
            }

            dbContext.Korisnici.Add(kor);
            await dbContext.SaveChangesAsync();
        }
    }
}
