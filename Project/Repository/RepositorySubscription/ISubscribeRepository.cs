using Project.Model;

namespace Project.Repository;

public interface ISubscribeRepository
{
    public Task Subscribe(Subscription subscription);
    public Task<Subscription?> GetSubscription(int id);
    Task PayForSubscription(Subscription subscription, decimal paymentDtoPayment);
    Task<Discount?> GetBestActiveDiscountForSubscription();
}