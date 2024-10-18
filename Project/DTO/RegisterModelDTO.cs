using Project.Model;

namespace Project.DTO;

public class RegisterModelDTO
{
    public string Login { get; set; }
    public string Password { get; set; }
    public RoleType RoleType { get; set; }
}