using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaPorudzbina : IEntityTypeConfiguration<Porudzbina>
    {
       

        public void Configure(EntityTypeBuilder<Porudzbina> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.AdresaIsporuke).IsRequired().HasMaxLength(50);

            builder.HasOne(p => p.Korisnik)
                .WithMany(p => p.Porudzbine)
                .HasForeignKey(p => p.IdKorisnika)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
