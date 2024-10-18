namespace Project.Model;

public class ClientPhysical : AbstractClient
{
    public string Surname { get; set; }
    public string PESEL { get; set; }
    public bool IsDeleted { get; set; }

}