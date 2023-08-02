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
            builder.HasKey(k => k.IdKorisnika);

            builder.Property(k => k.IdKorisnika).ValueGeneratedOnAdd();
            builder.Property(k => k.Ime).HasMaxLength(20).IsRequired();
            builder.Property(k => k.Prezime).IsRequired();
            builder.Property(k => k.KorisnickoIme).HasMaxLength(15).IsRequired();
            builder.HasIndex(k => k.KorisnickoIme).IsUnique();
            builder.Property(k => k.Email).IsRequired();
            builder.HasIndex(k => k.Email).IsUnique();
            builder.Property(k => k.Lozinka).IsRequired();
            builder.Property(k => k.PotvrdaLozinke).IsRequired();
            builder.Property(k => k.DatumRodjenja).IsRequired();
            builder.Property(k => k.Adresa).IsRequired();
            builder.Property(k => k.TipKorisnika).HasConversion(new EnumToStringConverter<TipKorisnika>());
            builder.Property(k => k.StatusVerifrikacije).HasConversion(new EnumToStringConverter<StatusVerifikacije>());
            builder.Property(k => k.Obrisan).HasDefaultValue(false);
            builder.Property(k => k.Verifikovan).HasDefaultValue(false);
            builder.Property(k => k.PotvrdaRegistracije).HasDefaultValue(false);
            builder.HasMany(k => k.Porudzbine)
                .WithOne(p => p.Korisnik)
                .HasForeignKey(p => p.IdKorisnika)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(k => k.Artikli)
               .WithOne(a => a.Korisnik)
               .HasForeignKey(a => a.IdKorisnika)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
