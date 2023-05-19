using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Data
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<Kupac> Kupci { get; set; }
        public DbSet<Administrator> Administratori { get; set; }
        public DbSet<Prodavac> Prodavci { get; set; }
        public DbSet<Kupac> Artikli { get; set; }
        public DbSet<Kupac> Porudzbine { get; set; }
        public DbSet<ArtikalIPorudzbina> ArtikliIPorudzbine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);

        }
    }
}
