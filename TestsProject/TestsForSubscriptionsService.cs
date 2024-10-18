using Moq;
using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;
using Project.Service;

namespace ProjectTests;

public class TestsForSubscriptionsService
{
    private readonly Mock<ISubscribeRepository> _subscriptionRepositoryMock;
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly Mock<IClientRepository> _clientRepositoryMock;

    private readonly Mock<ISubscriptionService> _subscriptionServiceMock;

    private readonly SubscriptionService _subscriptionService;

    public TestsForSubscriptionsService()
    {
        _contractRepositoryMock = new Mock<IContractRepository>();
        _clientRepositoryMock = new Mock<IClientRepository>();
        _subscriptionRepositoryMock = new Mock<ISubscribeRepository>();
        _subscriptionService = new SubscriptionService(_subscriptionRepositoryMock.Object, _contractRepositoryMock.Object, _clientRepositoryMock.Object);
    }

    [Fact]
    public async Task Subscribe_Should_Add_Subscription_To_Base()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 5,
            Price = 300
        };
        await _subscriptionService.Subscribe(subscribeAddDto);
        
        _subscriptionRepositoryMock.Verify(repo => repo.Subscribe(It.IsAny<Subscription>()), Times.Once);

    }
    [Fact]
    public async Task Subscribe_Should_Return_ClientNotFound()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync((AbstractClient?)null);

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 5,
            Price = 300
        };
        await Assert.ThrowsAsync<ClientNotFound>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_ClientNotFound_Because_Deleted()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical()
            {
                IsDeleted = true
            });

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 5,
            Price = 300
        };
        await Assert.ThrowsAsync<ClientNotFound>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_AlreadyHaveUnpaidContractWithThisProduct()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        var client = new ClientFirm()
        {
            Id = clientId,
            Subscriptions = new List<Subscription>
            {
                new Subscription()
                {
                    IdSoftware = idSoftware, 
                    IsCancelled = false
                }
            }
        };
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(client);

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 5,
            Price = 300
        };
        await Assert.ThrowsAsync<AlreadyHaveUnpaidContractWithThisProduct>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_SoftwareIsNotFound()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync((Software?)null);

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 5,
            Price = 300
        };
        await Assert.ThrowsAsync<SoftwareIsNotFound>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_NotCorrectRenewalPeriod_Too_Short()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 0,
            Price = 300
        };
        await Assert.ThrowsAsync<NotCorrectRenewalPeriod>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_NotCorrectRenewalPeriod_Too_Long()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 63,
            Price = 300
        };
        await Assert.ThrowsAsync<NotCorrectRenewalPeriod>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task Subscribe_Should_Return_NotPositiveValueForPrice()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        SubscribeAddDTO subscribeAddDto = new SubscribeAddDTO()
        {
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            RenewalPeriodInMonth = 15,
            Price = 0
        };
        await Assert.ThrowsAsync<NotPositiveValueForPrice>(() => _subscriptionService.Subscribe(subscribeAddDto));
 
    }
    [Fact]
    public async Task PayForSubscription_Should_Update_Payment_Subscription_In_Base()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m,
        });

        _subscriptionRepositoryMock.Verify(repo => repo.PayForSubscription(subscription, 100m), Times.Once);

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_ClientNotFound()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync((ClientPhysical?)null);
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<ClientNotFound>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_ClientNotFound_Because_Delete()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical()
        {
            IsDeleted = true
        });
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<ClientNotFound>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    [Fact]
         public async Task PayForSubscription_Should_Return_NotPositiveValueForPayment()
         {
             int clientId = 1;
             int idSubscription = 1;
             int idSoftware = 1;
     
             Subscription subscription = new Subscription()
             {
                 Id = idSubscription,
                 IdClient = clientId,
                 IdSoftware = idSoftware,
                 Name = "asd",
                 DateFrom = DateOnly.FromDateTime(DateTime.Now),
                 AlreadyPayed = 200,
                 RenewalPeriodInMonth = 5,
                 RealesedPayments = 2,
                 Price = 100,
                 IsCancelled = false
             };
     
             _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
             _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
             _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());
     
             await Assert.ThrowsAsync<NotPositiveValueForPayment>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
             {
                 IdClient = clientId,
                 Payment = 0m
             }));
     
         }
    [Fact]
    public async Task PayForSubscription_Should_Return_SubscriptionIsNotFound()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync((Subscription?)null);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<SubscriptionIsNotFound>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_EndOfSubscription()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 5,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<EndOfSubscription>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_PaymentAlreadyWasInThisPeriod()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 1,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<PaymentAlreadyWasInThisPeriod>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_TimeToPayExpired()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.Parse("2022-05-04"),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 1,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<TimeToPayExpired>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 100m
        }));

    }
    
    [Fact]
    public async Task PayForSubscription_Should_Return_NotOwnerOfSubscription()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _clientRepositoryMock.Setup(repo => repo.GetClient(142)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<NotOwnerOfSubscription>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = 142,
            Payment = 100m
        }));

    }
    [Fact]
    public async Task PayForSubscription_Should_Return_NotFullPriceForMonth()
    {
        int clientId = 1;
        int idSubscription = 1;
        int idSoftware = 1;

        Subscription subscription = new Subscription()
        {
            Id = idSubscription,
            IdClient = clientId,
            IdSoftware = idSoftware,
            Name = "asd",
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            AlreadyPayed = 200,
            RenewalPeriodInMonth = 5,
            RealesedPayments = 2,
            Price = 100,
            IsCancelled = false
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId)).ReturnsAsync(new ClientPhysical());
        _subscriptionRepositoryMock.Setup(repo => repo.GetSubscription(idSubscription)).ReturnsAsync(subscription);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware)).ReturnsAsync(new Software());

        await Assert.ThrowsAsync<NotFullPriceForMonth>(() => _subscriptionService.PayForSubscription(idSubscription, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 300m
        }));

    }
    
}