using Project.DTO;

namespace Project.Service;

public interface ISubscriptionService
{
    public Task Subscribe(SubscribeAddDTO subscribeAddDto);
    public Task PayForSubscription(int id, PaymentDTO paymentDto);
}