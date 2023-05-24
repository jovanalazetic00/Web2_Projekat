using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configuration
{
    public class KonfiguracijaArtikala : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();
            builder.Property(a => a.Naziv).IsRequired().HasMaxLength(50);
            builder.Property(a => a.Cijena).IsRequired();
            builder.Property(a => a.KolicinaArtikla).IsRequired();
            builder.Property(a => a.Obrisan).HasDefaultValue(false);

            builder.HasOne(a => a.Korisnik)
                .WithMany(a => a.Artikli)
                .HasForeignKey(a => a.IdKorisnika)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
