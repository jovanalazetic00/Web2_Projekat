using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces;
using Web_Projekat_PR111_2019.Models;
using static Raven.Client.Documents.Commands.MultiGet.GetRequest;

namespace Web_Projekat_PR111_2019.Repositories
{
    public class PorudzbinaRepository : IPorudzbinaRepository
    {
        private readonly DBContext dbContext;

        public PorudzbinaRepository(DBContext context)
        {
            dbContext = context;
        }
        public async Task CuvanjePromjena()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task<Porudzbina> GetPorudzbinaById(int id)
        {
            try
            {
                Porudzbina porudzbina = await dbContext.Porudzbine.Include(p => p.ArtikliIPorudzbine).FirstOrDefaultAsync(p => p.Id == id);
                return porudzbina;
            }
            catch (Exception ex)
            {
                
                return null;
            }
        }

        public async Task<List<Porudzbina>> GetSvePorudzbine()
        {
            try
            {
                List<Porudzbina> porudzbine = await dbContext.Porudzbine
                    .Include(p => p.ArtikliIPorudzbine)
                    .ThenInclude(aip => aip.Artikal)
                    .ToListAsync();

                return porudzbine;
            }
            catch (Exception e)
            {
                
                return null;
            }
        }

        public async Task<Porudzbina> KreiranjePorudzbine(Porudzbina porudzbina)
        {
            try
            {
                dbContext.Porudzbine.Add(porudzbina);
                await dbContext.SaveChangesAsync();
                return porudzbina;
            }
            catch (Exception e)
            {
                
                return null;
            }
        }
    }

        
}
