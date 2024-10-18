namespace Project.Model;

public abstract class AbstractClient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string TelephoneNumber { get; set; }

    public ICollection<Contract> Contracts = new List<Contract>();
    public ICollection<Subscription> Subscriptions = new List<Subscription>();

}