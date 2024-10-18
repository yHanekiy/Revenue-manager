namespace Project.DTO;

public class ContractCreateDTO
{
    public int IdSoftware { get; set; }
    public int IdClient { get; set; }

    public string DateTo { get; set; }
    public int YearsToBuy { get; set; }
    public int YearsToSupport { get; set; }
    public string Description { get; set; }
}