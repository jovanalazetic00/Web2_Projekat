using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaPorudzbinaIArtikala : IEntityTypeConfiguration<ArtikalIPorudzbina>
    {
        public void Configure(EntityTypeBuilder<ArtikalIPorudzbina> builder)
        {
            builder.HasKey(op => new { op.IDPorudzbine, op.IDArtikla });

            builder.HasOne(p => p.Porudzbina).WithMany(p => p.ArtikliIPorudzbine).IsRequired().OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(p => p.Artikal).WithMany(p => p.ArtikliIporudzbine).IsRequired().OnDelete(DeleteBehavior.NoAction);

        }
    }
}
