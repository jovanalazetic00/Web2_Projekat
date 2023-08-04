using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Repository
{
    public class ArtikalRepository : IArtikalRepository
    {
        private readonly DBContext dbContext;

        public ArtikalRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Artikal> AzurirajArtikal(Artikal artikal)
        {
            dbContext.Update(artikal);
            await dbContext.SaveChangesAsync();
            return artikal;
        }

        public async Task<Artikal> DobaviArtikalPoId(int id)
        {
            var artikal = await dbContext.Artikli.FirstOrDefaultAsync(a => a.ArtikalId == id);

            if (artikal == null)
            {
                return null;
            }
            return artikal;
        }

        public async Task<List<Artikal>> DobaviSveArtikle()
        {
            return await dbContext.Artikli.ToListAsync();
        }

        public async Task DodajArtikal(Artikal artikal)
        {
            if (artikal != null)
            {

                dbContext.Artikli.Add(artikal);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<Artikal> ObrisiArtikal(int id)
        {
            var artikal = dbContext.Artikli.FirstOrDefault(a => a.ArtikalId == id);

            if (artikal == null)
            {
                return null;
            }

            dbContext.Remove(artikal);
            await dbContext.SaveChangesAsync();
            return artikal;
        }
    }
}
