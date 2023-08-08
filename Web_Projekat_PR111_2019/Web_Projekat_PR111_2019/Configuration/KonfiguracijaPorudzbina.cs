using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Security.Cryptography;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaPorudzbina : IEntityTypeConfiguration<Porudzbina>
    {


        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(p => p.IdPorudzbine);
            builder.Property(p => p.IdPorudzbine).ValueGeneratedOnAdd();
            builder.Property(p => p.KomentarPorudzbine);
            builder.Property(p => p.AdresaIsporuke).HasMaxLength(40).IsRequired();
            builder.Property(p => p.CijenaPorudzbine).IsRequired();
            builder.Property(p => p.VrijemeIsporuke).IsRequired();
            builder.Property(p => p.VrijemePorudzbine).IsRequired();

            builder.Property(p => p.StatusPorudzbine).HasConversion(new EnumToStringConverter<StatusPorudzbine>());

            builder.HasOne(p => p.Korisnik)
                .WithMany(k => k.Porudzbine)
                .HasForeignKey(p => p.IdKorisnika)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.ArtikliIPorudzbine)
                .WithOne(s => s.Porudzbina)
                .HasForeignKey(s => s.IDPorudzbineAIP)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
