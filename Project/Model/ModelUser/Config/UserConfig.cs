using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Helper;

namespace Project.Model.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();

        builder.Property(e => e.Login).HasMaxLength(50).IsRequired();
        builder.Property(e => e.Password).IsRequired();
        builder.Property(e => e.Salt).IsRequired();
        builder.Property(e => e.RefreshToken).HasMaxLength(100).IsRequired();
        builder.Property(e => e.RefreshTokenExp).IsRequired();
        builder.Property(e => e.RoleType).IsRequired();

        builder.ToTable("User");
        
        Tuple<string, string> hashedPasswordAndSalt1 = SecurityHelpers.GetHashedPasswordAndSalt("123");
        Tuple<string, string> hashedPasswordAndSalt2 = SecurityHelpers.GetHashedPasswordAndSalt("456");


        List<User> users = new List<User>()
        {
            new User()
            {
                Id = 1,
                Login = "Jan",
                Password = hashedPasswordAndSalt1.Item1,
                Salt = hashedPasswordAndSalt1.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1),
                RoleType = RoleType.User
            },
            new User()
            {
                Id = 2,
                Login = "Anton",
                Password = hashedPasswordAndSalt2.Item1,
                Salt = hashedPasswordAndSalt2.Item2,
                RefreshToken = SecurityHelpers.GenerateRefreshToken(),
                RefreshTokenExp = DateTime.Now.AddDays(1),
                RoleType = RoleType.Admin
            }
        };

        builder.HasData(users);
    }
}