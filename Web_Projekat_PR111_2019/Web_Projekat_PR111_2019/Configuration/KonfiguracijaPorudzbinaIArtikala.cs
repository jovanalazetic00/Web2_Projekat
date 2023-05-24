using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaPorudzbinaIArtikala : IEntityTypeConfiguration<ArtikalIPorudzbina>
    {
        public void Configure(EntityTypeBuilder<ArtikalIPorudzbina> builder)
        {
            builder.HasKey(aip => new { aip.IDPorudzbine, aip.IDArtikla });

            builder.HasOne(aip => aip.Artikal)
                .WithMany(aip => aip.ArtikliIporudzbine)
                .HasForeignKey(aip => aip.IDArtikla)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(aip => aip.Porudzbina)
                .WithMany(aip => aip.ArtikliIPorudzbine)
                .HasForeignKey(aip => aip.IDPorudzbine)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
