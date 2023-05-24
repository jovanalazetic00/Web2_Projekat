using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<Korisnik>
    {
        public void Configure(EntityTypeBuilder<Korisnik> builder)
        {
            builder.HasKey(k => k.IdK);
            builder.Property(k => k.IdK).ValueGeneratedOnAdd();
            builder.Property(k => k.KorisnickoIme).IsRequired();
            builder.HasIndex(k => k.KorisnickoIme).IsUnique();
            builder.Property(k => k.Email).IsRequired();
            builder.Property(k => k.Lozinka).IsRequired();
            builder.Property(k => k.Ime).IsRequired().HasMaxLength(50);
            builder.Property(k => k.Prezime).IsRequired().HasMaxLength(30);
            builder.Property(k => k.DatumRodjenja).IsRequired();
            builder.Property(k => k.Adresa).IsRequired().HasMaxLength(30);
            builder.Property(k => k.TipKorisnika).HasConversion(new EnumToStringConverter<TipKorisnika>());
            builder.Property(k => k.StatusKorisnika).HasConversion(new EnumToStringConverter<StatusKorisnika>());
        }
    }
}
