using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Config;

public class SoftwareConfig : IEntityTypeConfiguration<Software>
{
    public void Configure(EntityTypeBuilder<Software> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Description).HasMaxLength(200).IsRequired();
        builder.Property(e => e.CurrentVersion).HasMaxLength(30).IsRequired();
        builder.Property(e => e.Category).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PurchasePricePerYear).IsRequired();

        builder.ToTable("Software");

        List<Software> softwares = new List<Software>()
        {
            new Software()
            {
                Id = 1,
                Name = "AddBlock",
                Description = "To block add",
                CurrentVersion = "8.0.0+",
                Category = "Useful",
                PurchasePricePerYear = 500
            },
            new Software()
            {
                Id = 2,
                Name = "MyTerShop",
                Description = "Place to create design",
                CurrentVersion = "RT+4",
                Category = "Design",
                PurchasePricePerYear = 2700
            },
            new Software()
            {
                Id = 3,
                Name = "GameThrone",
                Description = "PLace to educate how create game",
                CurrentVersion = "Full 1.0",
                Category = "Education",
                PurchasePricePerYear = 1500
            }
        };

        builder.HasData(softwares);
    }
}