using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web_Projekat_PR111_2019.Models;

namespace Web_Projekat_PR111_2019.Configurations
{
    public class KonfiguracijaArtikala : IEntityTypeConfiguration<Artikal>
    {
        public void Configure(EntityTypeBuilder<Artikal> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.KolicinaArtikla).IsRequired();
            builder.Property(a => a.Cijena).IsRequired();
            builder.Property(a => a.ArtikliIporudzbine).IsRequired();

            builder.HasOne(a => a.Prodavac).WithMany(a => a.Artikli).HasForeignKey(a => a.IDProdavca).IsRequired().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
