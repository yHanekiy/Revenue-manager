namespace Project.Model;

public class Software
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string CurrentVersion { get; set; }
    public string Category { get; set; }
    public decimal PurchasePricePerYear { get; set; }

    public ICollection<Contract> Contracts = new List<Contract>();
    public ICollection<Subscription> Subscriptions = new List<Subscription>();

}