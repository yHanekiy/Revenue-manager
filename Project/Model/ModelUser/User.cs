namespace Project.Model;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExp { get; set; }
    public RoleType RoleType { get; set; }

}