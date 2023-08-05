using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Repository
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly DBContext dbContext;

        public PorudzbinaRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
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

        public async Task<Porudzbina> DobaviPorudzbinuPoId(int id)
        {
            var porudzbina = await dbContext.Porudzbine.FirstOrDefaultAsync(p => p.IdPorudzbine == id);

            if (porudzbina == null)
            {
                return null;
            }
            return porudzbina;
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
    }
}
