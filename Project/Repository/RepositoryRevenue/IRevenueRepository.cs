using Project.Model;

namespace Project.Repository;

public interface IRevenueRepository
{
    public Task<decimal?> RevenueCalculationContracts();
    public Task<decimal?> RevenueCalculationSubscriptions();
    public Task<decimal?> RevenueCalculationContractsForProduct(int product);
    public Task<decimal?> RevenueCalculationSubscriptionsForProduct(int product);
    public Task<decimal?> PredictableRevenueCalculationContracts();
    public Task<List<Subscription>> PredictableRevenueCalculationNotCancelledSubscriptions();
    public Task<decimal?> RevenueCalculationCancelledSubscriptions();
    public Task<decimal?> PredictableRevenueCalculationContractsForProduct(int softwareId);
    public Task<List<Subscription>> PredictableRevenueCalculationSubscriptionsForProduct(int softwareId);
    public Task<decimal?> PredictableRevenueCalculationCancelledSubscriptionsForProduct(int softwareId);
}