namespace Project.Model;

public class Subscription
{
    public int Id { get; set; }
    public int IdClient { get; set; }
    public int IdSoftware { get; set; }
    public string Name { get; set; }
    public DateOnly DateFrom { get; set; }
    public decimal AlreadyPayed { get; set; }
    public int RenewalPeriodInMonth { get; set; }
    public int RealesedPayments { get; set; }
    public decimal Price { get; set; }
    public bool IsCancelled { get; set; }
    
    public Software Software { get; set; }
    public AbstractClient AbstractClient { get; set; }
}