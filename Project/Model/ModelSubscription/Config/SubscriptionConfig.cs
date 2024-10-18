using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Config;

public class SubscriptionConfig : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.DateFrom).IsRequired();
        builder.Property(e => e.AlreadyPayed).IsRequired();

        builder.Property(e => e.RenewalPeriodInMonth).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.IsCancelled).IsRequired();

        
        
        builder.HasOne(e => e.AbstractClient).WithMany(e => e.Subscriptions).HasForeignKey(e => e.IdClient)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.Software).WithMany(e => e.Subscriptions).HasForeignKey(e => e.IdSoftware)
            .OnDelete(DeleteBehavior.Restrict);



        builder.ToTable("Subscription");

        List<Subscription> subscriptions = new List<Subscription>()
        {
            new Subscription()
            {
                Id = 1,
                IdClient = 3,
                IdSoftware = 2,
                Name = "ExSub+",
                DateFrom = DateOnly.Parse("2024-05-27"),
                AlreadyPayed = 300,
                RenewalPeriodInMonth = 2,
                RealesedPayments = 1,
                Price = 300,
                IsCancelled = false
            },
            new Subscription()
            {
                Id = 2,
                IdClient = 3,
                IdSoftware = 1,
                DateFrom = DateOnly.Parse("2024-02-27"),
                AlreadyPayed = 494,
                Name = "MegaOka++",
                RenewalPeriodInMonth = 4,
                RealesedPayments = 2,
                Price = 260,
                IsCancelled = true
            },
            new Subscription()
            {
                Id = 3,
                IdClient = 3,
                IdSoftware = 2,
                Name = "Turtle",
                AlreadyPayed = 1300,
                DateFrom = DateOnly.Parse("2023-08-27"),
                RenewalPeriodInMonth = 3,
                RealesedPayments = 3,
                Price = 450,
                IsCancelled = false
            }
        };

        builder.HasData(subscriptions);
    }
}