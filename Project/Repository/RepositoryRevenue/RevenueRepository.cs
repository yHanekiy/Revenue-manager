using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Model;

namespace Project.Repository;

public class RevenueRepository(APBDContext apbdContext) : IRevenueRepository
{
    public async Task<decimal?> RevenueCalculationContracts()
    {
        return await apbdContext.Contracts.Where(x => x.IsAlreadyPaid).SumAsync(x => x.Price);
    }

    public async Task<decimal?> RevenueCalculationSubscriptions()
    {
        return await apbdContext.Subscriptions.SumAsync(x => x.AlreadyPayed);
    }

    public async Task<decimal?> RevenueCalculationContractsForProduct(int idProduct)
    {
        return await apbdContext.Contracts.Where(x => x.IsAlreadyPaid && x.IdSoftware == idProduct)
            .SumAsync(x => x.Price);

    }

    public async Task<decimal?> RevenueCalculationSubscriptionsForProduct(int idProduct)
    {
        return await apbdContext.Subscriptions.Where(x => x.IdSoftware == idProduct).SumAsync(x => x.AlreadyPayed);
    }

    public async Task<decimal?> PredictableRevenueCalculationContracts()
    {
        return await apbdContext.Contracts
            .Where(x => x.DateTo >= DateOnly.FromDateTime(DateTime.Now) || x.IsAlreadyPaid).SumAsync(x => x.Price);
    }

    public async Task<List<Subscription>> PredictableRevenueCalculationNotCancelledSubscriptions()
    {
        return await apbdContext.Subscriptions.Where(x => !x.IsCancelled).ToListAsync();
    }

    public async Task<decimal?> RevenueCalculationCancelledSubscriptions()
    {
        return await apbdContext.Subscriptions.Where(x=>x.IsCancelled).SumAsync(x=>x.AlreadyPayed);
    }

    public async Task<decimal?> PredictableRevenueCalculationContractsForProduct(int softwareId)
    {
        return await apbdContext.Contracts.Where(x=>(x.DateTo>=DateOnly.FromDateTime(DateTime.Now) || x.IsAlreadyPaid) && x.IdSoftware == softwareId)
            .SumAsync(x => x.Price);
 
    }

    public async Task<List<Subscription>> PredictableRevenueCalculationSubscriptionsForProduct(int softwareId)
    {
        return await apbdContext.Subscriptions.Where(x=>!x.IsCancelled && x.IdSoftware==softwareId).ToListAsync();
    }
    public async Task<decimal?> PredictableRevenueCalculationCancelledSubscriptionsForProduct(int softwareId)
    {
        return await apbdContext.Subscriptions.Where(x=>x.IsCancelled && x.IdSoftware==softwareId).SumAsync(x=>x.AlreadyPayed);
    }
}