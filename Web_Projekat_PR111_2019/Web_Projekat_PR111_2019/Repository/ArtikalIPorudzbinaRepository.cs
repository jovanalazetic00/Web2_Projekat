using System.Net.NetworkInformation;
using Web_Projekat_PR111_2019.Data;
using Web_Projekat_PR111_2019.Interfaces.IRepository;
using Web_Projekat_PR111_2019.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Web_Projekat_PR111_2019.Repository
{
    public class ArtikalIPorudzbinaRepository : IArtikalIPorudzbinaRepository
    {
        private readonly DBContext dbContext;

        public ArtikalIPorudzbinaRepository(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ArtikalIPorudzbina>> DobaviStavkePorudzbine(int idPorudzbine)
        {
            var porudzbine = await dbContext.ArtikliIPorudzbine.Where(s => s.IDPorudzbineAIP == idPorudzbine).ToListAsync();
            return porudzbine;
        }
    }
}   
