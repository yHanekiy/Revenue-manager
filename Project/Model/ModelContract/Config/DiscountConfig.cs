using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Config;

public class DiscountConfig : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Offer).IsRequired();
        builder.Property(e => e.Value).HasMaxLength(30).IsRequired();
        builder.Property(e => e.DateFrom).IsRequired();
        builder.Property(e => e.DateTo).IsRequired();

        builder.ToTable("Discount");

        List<Discount> discounts = new List<Discount>()
        {
            new Discount()
            {
                Id = 1,
                Name = "Black Friday",
                Offer = TypePayment.Subscription,
                Value = 10,
                DateFrom = DateOnly.Parse("2024-08-12"),
                DateTo = DateOnly.Parse("2024-11-12")
            },
            new Discount()
            {
                Id = 2,
                Name = "Super price",
                Offer = TypePayment.Together,
                Value = 50,
                DateFrom = DateOnly.Parse("2024-06-25"),
                DateTo = DateOnly.Parse("2024-06-29")
            }
        };

        builder.HasData(discounts);
    }
}