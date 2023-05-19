using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaKupaca : IEntityTypeConfiguration<Kupac>
    {
        public void Configure(EntityTypeBuilder<Kupac> builder)
        {
            builder.HasKey(u => u.KorisnickoIme);
            builder.Property(u => u.KorisnickoIme).IsRequired();
            builder.HasIndex(u => u.KorisnickoIme).IsUnique();
            builder.Property(u => u.ImePrezime).IsRequired().HasMaxLength(100);
            builder.Property(u => u.DatumRodjenja).IsRequired();
            builder.Property(u => u.Adresa).IsRequired().HasMaxLength(80);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(40);
            builder.Property(u => u.Lozinka).IsRequired().HasMaxLength(20);

            builder.HasMany(c => c.Porudzbine).WithOne(o => o.Kupac).HasForeignKey(o => o.IDKupca).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(c => c.Administrator).WithMany(c => c.Kupci).HasForeignKey(c => c.KorisnickoIme).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
