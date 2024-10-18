using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Model;

namespace Project.Repository;

public class SubscribeRepository(APBDContext apbdContext) : ISubscribeRepository
{
    public async Task Subscribe(Subscription subscription)
    {
        await apbdContext.Subscriptions.AddAsync(subscription);
        await apbdContext.SaveChangesAsync();
    }

    public async Task<Subscription?> GetSubscription(int id)
    {
        return await apbdContext.Subscriptions.SingleOrDefaultAsync(x => x.Id == id && !x.IsCancelled);
    }

    public async Task PayForSubscription(Subscription subscription, decimal paymentDtoPayment)
    {
       subscription.RealesedPayments += 1;
       subscription.AlreadyPayed += paymentDtoPayment;
       await apbdContext.SaveChangesAsync();
    }

    public async Task<Discount?> GetBestActiveDiscountForSubscription()
    {
        return await apbdContext.Discounts.Where(x=>x.DateFrom <= DateOnly.FromDateTime(DateTime.Now) && x.DateTo >= DateOnly.FromDateTime(DateTime.Now) && (x.Offer == TypePayment.Subscription || x.Offer == TypePayment.Together))
            .OrderByDescending(x => x.Value).FirstOrDefaultAsync();

    }
}