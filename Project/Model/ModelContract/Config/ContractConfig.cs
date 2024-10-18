using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Config;

public class ContractConfig : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.DateFrom).IsRequired();
        builder.Property(e => e.DateTo).IsRequired();
        builder.Property(e => e.YearsToBuy).IsRequired();
        builder.Property(e => e.YearsToSupport).IsRequired();
        builder.Property(e => e.AlreadyPaid).IsRequired();
        builder.Property(e => e.Price).IsRequired();
        builder.Property(e => e.Desciption).HasMaxLength(50).IsRequired();
        builder.Property(e => e.IsAlreadyPaid).IsRequired();
        
        builder.HasOne(e => e.Software).WithMany(e => e.Contracts).HasForeignKey(e => e.IdSoftware)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.AbstractClient).WithMany(e => e.Contracts).HasForeignKey(e => e.IdClient)
            .OnDelete(DeleteBehavior.Restrict);


        builder.ToTable("Contract");

        List<Contract> contracts = new List<Contract>()
        {
            new Contract()
            {
              Id  = 1,
              IdClient = 1,
              IdSoftware = 3,
              DateFrom = DateOnly.Parse("2024-03-15"),
              DateTo = DateOnly.Parse("2024-03-24"),
              YearsToBuy = 2,
              YearsToSupport = 2,
              AlreadyPaid = 2400,
              Price = 1500 * 2 + 1000 * 2,
              Desciption = "Newest version",
              IsAlreadyPaid = false
            },
            new Contract()
            {
                Id  = 2,
                IdClient = 4,
                IdSoftware = 2,
                DateFrom = DateOnly.Parse("2024-04-12"),
                DateTo = DateOnly.Parse("2024-04-19"),
                YearsToBuy = 1,
                YearsToSupport = 0,
                AlreadyPaid = 2700,
                Price = 2700 * 1 + 1000 * 0,
                Desciption = "All is good",
                IsAlreadyPaid = true
            },
        };

        builder.HasData(contracts);
    }
}