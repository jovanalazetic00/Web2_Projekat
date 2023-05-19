using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaProdavaca : IEntityTypeConfiguration<Prodavac>
    {
        public void Configure(EntityTypeBuilder<Prodavac> builder)
        {
            builder.HasKey(u => u.KorisnickoIme);
            builder.Property(u => u.KorisnickoIme).IsRequired();
            builder.HasIndex(u => u.KorisnickoIme).IsUnique();
            builder.Property(u => u.ImePrezime).IsRequired().HasMaxLength(100);
            builder.Property(u => u.DatumRodjenja).IsRequired();
            builder.Property(u => u.Adresa).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Lozinka).IsRequired().HasMaxLength(100);

            builder.HasOne(s => s.Administrator).WithMany(s => s.Prodavci).IsRequired().OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(s => s.Artikli).WithOne(p => p.Prodavac).HasForeignKey(p=>p.IDProdavca).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
