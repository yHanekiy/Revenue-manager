using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Configuration;

public class ClientFirmConfig : IEntityTypeConfiguration<ClientFirm>
{
    public void Configure(EntityTypeBuilder<ClientFirm> builder)
    {
        builder.Property(e => e.KRS).HasMaxLength(14).IsRequired();
        
        List<ClientFirm> clientFirms = new List<ClientFirm>()
        {
            new ClientFirm()
            {
                Id = 1,
                Name = "QWERTY",
                Address = "Warsaw",
                Email = "qwerty@gmail.com",
                TelephoneNumber = "785234542",
                KRS = "95478902934512"
            },
            new ClientFirm()
            {
                Id = 3,
                Name = "ZXCVBN",
                Address = "Tokyo",
                Email = "zxcvbn@gmail.com",
                TelephoneNumber = "203945813",
                KRS = "902385195"
            }
        };

        builder.HasData(clientFirms);
    }
}