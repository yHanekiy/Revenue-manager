using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;

namespace Project.Service;

public class SubscriptionService(ISubscribeRepository subscribeRepository, IContractRepository contractRepository, IClientRepository clientRepository) : ISubscriptionService
{
    public async Task Subscribe(SubscribeAddDTO subscribeAddDto)
    {
        AbstractClient? client = await clientRepository.GetClient(subscribeAddDto.IdClient);
        if (client == null)
        {
            throw new ClientNotFound();
        }

        CheckClientInBase(client);
        CheckAlreadyHaveUnpaidSubcriptionWithThisProduct(subscribeAddDto, client);
        if (await contractRepository.GetSoftwareById(subscribeAddDto.IdSoftware) == null)
        {
            throw new SoftwareIsNotFound();
        }
        CheckBorderOfRenewalPeriod(subscribeAddDto.RenewalPeriodInMonth);
        CheckPositiveValuePrice(subscribeAddDto.Price);
        
        

        Subscription subscription = new Subscription()
        {
            IdClient = subscribeAddDto.IdClient,
            IdSoftware = subscribeAddDto.IdSoftware,
            Name = subscribeAddDto.Name,
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = await CalculatePaymentWithDiscountsFirstTime(client,subscribeAddDto.Price),
            RenewalPeriodInMonth = subscribeAddDto.RenewalPeriodInMonth,
            RealesedPayments = 1,
            Price = subscribeAddDto.Price,
            IsCancelled = false
        };
        await subscribeRepository.Subscribe(subscription);
    }

    public async Task PayForSubscription(int id, PaymentDTO paymentDto)
    {
        AbstractClient? client = await clientRepository.GetClient(paymentDto.IdClient);
        if (client == null)
        {
            throw new ClientNotFound();
        }
        CheckClientInBase(client);
        CheckPositiveValuePayment(paymentDto.Payment);
        Subscription? subscription = await subscribeRepository.GetSubscription(id);
        if (subscription == null)
        {
            throw new SubscriptionIsNotFound();
        }

        CheckEndSubscription(subscription);
        CheckTimeForPayment(subscription);
        CheckSubscriptionBelongsToClient(paymentDto.IdClient, subscription);
        CheckFullPrice(subscription.Price,paymentDto.Payment);

        await subscribeRepository.PayForSubscription(subscription, CalculatePaymentWithDiscounts(client,paymentDto.Payment));
    }
    
    private void CheckAlreadyHaveUnpaidSubcriptionWithThisProduct(SubscribeAddDTO subscribeAddDto,AbstractClient client)
    {
        if (client!.Subscriptions.Any(x =>
                x.IdSoftware == subscribeAddDto.IdSoftware && !x.IsCancelled))
        {
            throw new AlreadyHaveUnpaidContractWithThisProduct();
        }
    }

    private void CheckEndSubscription(Subscription subscription)
    {
        if (subscription.RealesedPayments == subscription.RenewalPeriodInMonth)
        {
            throw new EndOfSubscription();
        }
    }

    private void CheckClientInBase(AbstractClient client)
    {
        if (client is ClientPhysical clientPhysical)
        {
            if (clientPhysical.IsDeleted)
            {
                throw new ClientNotFound();
            }
        }
    }
    public async Task<decimal> CalculatePaymentWithDiscountsFirstTime(AbstractClient client, decimal payment)
    {
        Discount? discount = await subscribeRepository.GetBestActiveDiscountForSubscription();
        decimal valueDiscount = client!.Contracts.Any(x=>x.IsAlreadyPaid) ? 5m: client!.Subscriptions.Count >0? 5m:0m;
        if (discount!=null)
        {
            valueDiscount = (discount.Value+valueDiscount) / 100;
        }

        return payment - payment * valueDiscount;
    }

    public decimal CalculatePaymentWithDiscounts(AbstractClient client, decimal payment)
    {
        decimal valueDiscount = client!.Contracts.Any(x=>x.IsAlreadyPaid) ? 5m: client!.Subscriptions.Count >0? 5m:0m;
        return payment - payment * valueDiscount / 100;
    }


    private void CheckFullPrice(decimal price, decimal payment)
    {
        if (price != payment)
        {
            throw new NotFullPriceForMonth();
        }
    }

    private void CheckTimeForPayment(Subscription subscription)
    {
        if (subscription.DateFrom.AddMonths(subscription.RealesedPayments-1) <= DateOnly.FromDateTime(DateTime.Now) &&
            subscription.DateFrom.AddMonths(subscription.RealesedPayments) >= DateOnly.FromDateTime(DateTime.Now))
        {
            throw new PaymentAlreadyWasInThisPeriod();
        }

        if (subscription.DateFrom.AddMonths(subscription.RealesedPayments+1) <= DateOnly.FromDateTime(DateTime.Now))
        {
            throw new TimeToPayExpired();
        }
    }

    private void CheckBorderOfRenewalPeriod(int renewalPeriod)
    {
        if (renewalPeriod < 1 || renewalPeriod > 24)
        {
            throw new NotCorrectRenewalPeriod();
        }
    }

    private void CheckPositiveValuePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new NotPositiveValueForPrice();
        }
    }
    private void CheckPositiveValuePayment(decimal payment)
    {
        if (payment <= 0)
        {
            throw new NotPositiveValueForPayment();
        }
    }

    private void CheckSubscriptionBelongsToClient(int idClient, Subscription subscription)
    {
        if (subscription.IdClient != idClient)
        {
            throw new NotOwnerOfSubscription();
        }
    }
}