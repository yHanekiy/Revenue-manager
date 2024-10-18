using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Model.Configuration;

public class ClientAbstractConfig : IEntityTypeConfiguration<AbstractClient>
{
    
    public void Configure(EntityTypeBuilder<AbstractClient> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();
        
        builder.Property(e => e.Name).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Address).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Email).HasMaxLength(50).IsRequired();
        builder.Property(e => e.TelephoneNumber).HasMaxLength(50).IsRequired();

        builder.ToTable("Client");
    }
}