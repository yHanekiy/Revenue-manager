using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Configuration;

public class ClientPhysicalConfig : IEntityTypeConfiguration<ClientPhysical>
{
    public void Configure(EntityTypeBuilder<ClientPhysical> builder)
    {
        builder.Property(e => e.Surname).HasMaxLength(50).IsRequired();
        builder.Property(e => e.PESEL).HasMaxLength(11).IsRequired();
        builder.Property(e => e.IsDeleted).IsRequired();
        
        List<ClientPhysical> clientPhysicals = new List<ClientPhysical>()
        {
            new ClientPhysical()
            {
                Id = 2,
                Name = "Alan",
                Surname = "Po",
                Address = "London",
                Email = "alanPo@gmail.com",
                TelephoneNumber = "123456789",
                PESEL = "74830591354",
                IsDeleted = false
            },
            new ClientPhysical()
            {
                Id = 4,
                Name = "Edgar",
                Surname = "Ton",
                Address = "Paris",
                Email = "tonoEefs@gmail.com",
                TelephoneNumber = "987654321",
                PESEL = "83591050021",
                IsDeleted = false
            },
        };

        builder.HasData(clientPhysicals);
    }
}