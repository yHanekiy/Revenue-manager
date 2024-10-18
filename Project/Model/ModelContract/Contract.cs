namespace Project.Model;

public class Contract
{
    public int Id { get; set; }
    public int IdClient { get; set; }
    public int IdSoftware { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public int YearsToBuy { get; set; }
    public int YearsToSupport { get; set; }
    public decimal AlreadyPaid { get; set; }

    public decimal Price { get; set; }
    public string Desciption { get; set; }
    public bool IsAlreadyPaid { get; set; }
    
    public Software Software { get; set; }
    public AbstractClient AbstractClient { get; set; }

}
