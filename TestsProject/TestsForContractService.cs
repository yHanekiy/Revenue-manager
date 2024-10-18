using Moq;
using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;
using Project.Service;

namespace ProjectTests;

public class TestsForContractService
{
    private readonly Mock<IContractRepository> _contractRepositoryMock;
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly Mock<IContractService> _contractServiceMock;

    private readonly ContractService _contractService;

    public TestsForContractService()
    {
        _contractRepositoryMock = new Mock<IContractRepository>();
        _clientRepositoryMock = new Mock<IClientRepository>();
        _contractServiceMock = new Mock<IContractService>();
        _contractService = new ContractService(_contractRepositoryMock.Object, _clientRepositoryMock.Object);
    }

    [Fact]
    public async Task CreateContract_Should_Add_Contract_To_Base()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await _contractService.CreateContract(contractCreateDto);
        
        _contractRepositoryMock.Verify(repo => repo.CreateContract(It.IsAny<Contract>()), Times.Once);

    }
    [Fact]
    public async Task CreateContract_Should_Return_ClientNotFound()
    {
        int clientId = 1;
        int idSoftware = 1;
        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync((AbstractClient?)null);

        await Assert.ThrowsAsync<ClientNotFound>(() => _contractService.CreateContract(contractCreateDto));

    }

    [Fact]
    public async Task CreateContract_Should_Return_ClientNotFound_Because_Deleted()
    {
        int clientId = 1;
        int idSoftware = 1;

        var clientPhysical = new ClientPhysical
        {
            Id = clientId,
            Name = "name",
            Surname = "surname",
            Address = "address",
            Email = "email.@gmail.com",
            TelephoneNumber = "000000000",
            PESEL = "00000000000",
            IsDeleted = true 
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(clientPhysical);
        _clientRepositoryMock.Setup(repo => repo.GetClientPhysicalFromBase(clientId))
            .ReturnsAsync(clientPhysical);

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };

        await Assert.ThrowsAsync<ClientNotFound>(() => _contractService.CreateContract(contractCreateDto));

    }

    [Fact]
    public async Task CreateContract_Should_Return_NotCorrectDataFormat()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "asgadfsadf",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<NotCorrectDataFormat>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_NotEnoughTimeContract()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2020-04-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<NotEnoughTimeContract>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_TooMuchTimeContract()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2154-04-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<TooMuchTimeContract>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_NotPossibleSupport()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-24",
            YearsToBuy = 1,
            YearsToSupport = -1,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<NotPossibleSupport>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_NotPossibleBuy()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-24",
            YearsToBuy = 0,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<NotPossibleBuy>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_SoftwareIsNotFound()
    {
        int clientId = 1;
        int idSoftware = 1;
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync((Software?)null);

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-24",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };
        
        await Assert.ThrowsAsync<SoftwareIsNotFound>(() => _contractService.CreateContract(contractCreateDto));

    }
    [Fact]
    public async Task CreateContract_Should_Return_AlreadyHaveUnpaidContractWithThisProduct()
    {
        int clientId = 1;
        int idSoftware = 1;

        var client = new ClientFirm()
        {
            Id = clientId,
            Contracts = new List<Contract>
            {
                new Contract
                {
                    IdSoftware = idSoftware,
                    DateTo = DateOnly.FromDateTime(DateTime.Now.AddDays(1)) // Ensure DateTo is in the future
                }
            }
        };

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(client);
        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        ContractCreateDTO contractCreateDto = new ContractCreateDTO
        {
            IdSoftware = idSoftware,
            IdClient = clientId,
            DateTo = "2024-07-04",
            YearsToBuy = 1,
            YearsToSupport = 0,
            Description = "description"
        };

        await Assert.ThrowsAsync<AlreadyHaveUnpaidContractWithThisProduct>(() => _contractService.CreateContract(contractCreateDto));

    }

    [Fact]
    public async Task MakePaymentForContract_NotAll_Should_Make_Payment_For_Contract()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;

        Contract? contract = new Contract()
        {
            Id = idContract,
            IdClient = clientId,
            IdSoftware = idSoftware,
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            DateTo = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
            YearsToBuy = 1,
            YearsToSupport = 0,
            AlreadyPaid = 0,
            Price = 5000,
            Desciption = "asd",
            IsAlreadyPaid = false
        };
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(contract);
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        
        await _contractService.MakePaymentForContract(idContract, new PaymentDTO()
        {
            IdClient = clientId,
            Payment = 4000
        });

        _contractRepositoryMock.Verify(repo => repo.MakePaymentForContract(contract, 4000), Times.Once);

    }
    [Fact]
    public async Task FinishContractPayment_All_Should_Make_Payment_For_Contract()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        int paymentAmount = 6000;

        Contract contract = new Contract
        {
            Id = idContract,
            IdClient = clientId,
            IdSoftware = idSoftware,
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            DateTo = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
            YearsToBuy = 1,
            YearsToSupport = 0,
            AlreadyPaid = 0,
            Price = 5000,
            Desciption = "asd",
            IsAlreadyPaid = false
        };

        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(contract);

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());

        await _contractService.MakePaymentForContract(idContract, new PaymentDTO
        {
            IdClient = clientId,
            Payment = paymentAmount
        });
        
        _contractRepositoryMock.Verify(repo => repo.FinishContractPayment(contract, contract.Price-contract.AlreadyPaid), Times.Once);

    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_ContractIsNotFound()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync((Contract?)null);
        
        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<ContractIsNotFound>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
            );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_ClientIsNotFound_Because_Deleted()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract());

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientPhysical()
            {
                IsDeleted = true
            });

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<ClientNotFound>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
        );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_ClientIsNotFound()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract());

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync((AbstractClient?)null);

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<ClientNotFound>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
        );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_NotOwnerOfContract()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract()
            {
                IdClient = 2
            });

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientFirm());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<NotOwnerOfContract>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
        );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_ContractIsAlreadyPaid()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract()
            {
                IdClient = clientId,
                IsAlreadyPaid = true
            });

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientFirm());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<ContractIsAlreadyPaid>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
        );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_TimeToPayExpired()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract()
            {
                IdClient = clientId,
                DateTo = DateOnly.Parse("2000-05-05")
            });

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientFirm());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<TimeToPayExpired>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = 1000
            })
        );
    }
    [Fact]
    public async Task MakePaymentForContract_NotAll_Return_NotPositiveValueForPayment()
    {
        int idContract = 1;
        int clientId = 1;
        int idSoftware = 1;
        
        _contractRepositoryMock.Setup(repo => repo.GetContract(idContract))
            .ReturnsAsync(new Contract()
            {
                IdClient = clientId,
                DateTo = DateOnly.FromDateTime(DateTime.Now.AddDays(10))
            });

        _clientRepositoryMock.Setup(repo => repo.GetClient(clientId))
            .ReturnsAsync(new ClientFirm());

        _contractRepositoryMock.Setup(repo => repo.GetSoftwareById(idSoftware))
            .ReturnsAsync(new Software());
        
        await Assert.ThrowsAsync<NotPositiveValueForPayment>(() => _contractService.MakePaymentForContract(idContract, new PaymentDTO
            {
                IdClient = clientId,
                Payment = -50
            })
        );
    }
    
    
}