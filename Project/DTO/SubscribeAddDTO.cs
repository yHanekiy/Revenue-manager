namespace Project.DTO;

public class SubscribeAddDTO
{
    public int IdClient { get; set; }
    public int IdSoftware { get; set; }
    public string Name { get; set; }
    public int RenewalPeriodInMonth { get; set; }
    public decimal Price { get; set; }
}