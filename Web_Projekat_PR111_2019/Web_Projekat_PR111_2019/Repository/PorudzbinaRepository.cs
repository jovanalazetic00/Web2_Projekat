using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Repository
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly DBContext dbContext;
        private readonly IArtikalIPorudzbinaRepository artPorRepository;

        public PorudzbinaRepository(DBContext dbContext, IArtikalIPorudzbinaRepository artPorRepository)
        {
            this.dbContext = dbContext;
            this.artPorRepository = artPorRepository;
        }

        public async Task<Porudzbina> AzurirajPorudzbinu(Porudzbina porudzbina)
        {
            if (porudzbina.CijenaPorudzbine <= 0)
            {
                throw new Exception("Cijena porudzbine mora biti veća od 0");
            }


            dbContext.Update(porudzbina);
            await dbContext.SaveChangesAsync();
            return porudzbina;
        }

        public async Task<List<Artikal>> DobaviArtikleOdPorudzbine(int idPorudzbine)
        {
            var stavke = await dbContext.ArtikliIPorudzbine
                .Where(s => s.IDPorudzbineAIP == idPorudzbine)
                .ToListAsync();

            var artikalIds = stavke.Select(s => s.IDArtiklaAIP).ToList();

            var artikli = await dbContext.Artikli
                .Where(a => artikalIds.Contains(a.ArtikalId))
                .ToListAsync();

            return artikli;
        }

        public async Task<List<Artikal>> DobaviArtiklePorudzbineProdavca(int idPorudzbine, int idProdavca)
        {
            var stavke = await dbContext.ArtikliIPorudzbine
                   .Where(s => s.IDPorudzbineAIP == idPorudzbine)
                   .ToListAsync();

            var artikalIds = stavke.Select(s => s.IDArtiklaAIP).ToList();

            var artikli = await dbContext.Artikli
                  .Where(a => artikalIds.Contains(a.ArtikalId) && a.IdKorisnika == idProdavca)
                  .ToListAsync();

            return artikli;
        }

        public async Task<Porudzbina> DobaviPorudzbinuPoId(int id)
        {
            var porudzbina = await dbContext.Porudzbine.FirstOrDefaultAsync(p => p.IdPorudzbine == id);

            if (porudzbina == null)
            {
                return null;
            }
            return porudzbina;
        }

        public async Task<List<Porudzbina>> DobaviPrethodnePorudzbineKupca(int id)
        {
            var svePorudzbine = await dbContext.Porudzbine.ToListAsync();


            if (svePorudzbine == null)
            {
                throw new Exception("Tabela je prazna");
            }

            var prethodnePorudzbine = new List<Porudzbina>();
            DateTime trenutnoVrijeme = DateTime.Now;


            foreach (var porudzbina in svePorudzbine)
            {
                if (porudzbina.IdKorisnika.ToString() == null)
                {
                    throw new Exception("Id je null");
                }
                else
                {
                    if (porudzbina.IdKorisnika == id && porudzbina.StatusPorudzbine != StatusPorudzbine.Otkazana)
                    {

                        var razlika = trenutnoVrijeme - porudzbina.VrijemePorudzbine;
                        if (razlika.TotalHours >= 1)
                        {
                            porudzbina.StatusPorudzbine = StatusPorudzbine.Prihvacena;
                            dbContext.Update(porudzbina);
                            prethodnePorudzbine.Add(porudzbina);
                        }
                    }

                }
            }
            await dbContext.SaveChangesAsync();

            return prethodnePorudzbine;
        }

        public async Task<List<Porudzbina>> DobaviSvePorudzbine()
        {
            return await dbContext.Porudzbine.ToListAsync();
        }

        public async Task DodajPoruzbinu(Porudzbina porudzbina)
        {
            if (porudzbina != null)
            {

                dbContext.Porudzbine.Add(porudzbina);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Porudzbina>> MojePorudzbine(int id)
        {
            var svePorudzbine = await dbContext.Porudzbine.ToListAsync();

            return svePorudzbine;
        }

        public async Task<List<Porudzbina>> NovePorudzineOdProdavca(int id)
        {
            var svePorudzbineProdavca = await dbContext.Porudzbine
            .Where(p => p.StatusPorudzbine != StatusPorudzbine.Otkazana)
            .ToListAsync();

            return svePorudzbineProdavca;
        }

        public async Task<Porudzbina> ObrisiPorudzbinu(int id)
        {
            var porudzbina = await dbContext.Porudzbine.FirstOrDefaultAsync(p => p.IdPorudzbine == id);

            if (porudzbina == null)
            {
                return null;
            }

            dbContext.Remove(porudzbina);
            await dbContext.SaveChangesAsync();
            return porudzbina;
        }

        public async Task OtkazivanjePorudzbinu(int id)
        {
            var porudzbina = await dbContext.Porudzbine.FirstOrDefaultAsync(p => p.IdPorudzbine == id);

            if (porudzbina == null)
            {
                throw new Exception("Porudzbina ne postoji u bazi!");
            }


            var stavke = await artPorRepository.DobaviStavkePorudzbine(id);


            foreach (var stavka in stavke)
            {
                var artikal = await dbContext.Artikli
                    .FirstOrDefaultAsync(a => a.ArtikalId == stavka.IDArtiklaAIP);

                if (artikal != null)
                {
                    artikal.KolicinaArtikla += stavka.KolicinaArtikla;
                    dbContext.Update(artikal);
                }
            }

            porudzbina.StatusPorudzbine = StatusPorudzbine.Otkazana;
            dbContext.Update(porudzbina);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Porudzbina>> SvePorudzbineOdProdavca(int id)
        {
            var porudzbine = await DobaviSvePorudzbine();


            if (porudzbine == null)
            {
                throw new Exception("Ne postoji nijedna porudzbina u bazi");
            }
            return porudzbine;
        }
    }
}
