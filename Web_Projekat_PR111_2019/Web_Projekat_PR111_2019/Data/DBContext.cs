using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Data
{
    public class DBContext:DbContext
    {
        public DBContext()
        {

        }
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Artikal> Artikli { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
        public DbSet<ArtikalIPorudzbina> ArtikliIPorudzbine { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);

        }
    }
}
