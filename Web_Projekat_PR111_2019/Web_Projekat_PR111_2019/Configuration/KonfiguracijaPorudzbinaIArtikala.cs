using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaPorudzbinaIArtikala : IEntityTypeConfiguration<ArtikalIPorudzbina>
    {
        public void Configure(EntityTypeBuilder<ArtikalIPorudzbina> builder)
        {
            builder.HasKey(s => new { s.IDPorudzbine, s.IDArtikla });

            builder.HasOne(s => s.Artikal)
             .WithMany(a => a.ArtikliIPorudzbine)
             .HasForeignKey(s => s.IDArtikla)
             .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(s => s.Porudzbina)
                .WithMany(p => p.ArtikliIPorudzbine)
                .HasForeignKey(s => s.IDPorudzbine)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
