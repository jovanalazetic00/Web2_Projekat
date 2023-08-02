using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configuration
{
    public class KonfiguracijaArtikala : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(p => p.ArtikalId);
            builder.Property(a => a.ArtikalId).ValueGeneratedOnAdd();
            builder.Property(a => a.Naziv).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Cijena).IsRequired();
            builder.Property(a => a.KolicinaArtikla).IsRequired();
            builder.Property(a => a.Opis);

            builder.HasOne(a => a.Korisnik)
               .WithMany(k => k.Artikli)
               .HasForeignKey(a => a.IdKorisnika)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(a => a.ArtikliIPorudzbine)
                .WithOne(s => s.Artikal)
                .HasForeignKey(s => s.IDArtikla)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
