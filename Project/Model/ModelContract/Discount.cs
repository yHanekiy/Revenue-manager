namespace Project.Model;

public class Discount
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TypePayment Offer { get; set; }
    public decimal Value { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }

}