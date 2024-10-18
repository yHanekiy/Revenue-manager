using Project.Context;
using Project.Model;
using Project.Repository;
using Moq;
using Project.DTO;
using Project.Error;
using Project.Service;

namespace ProjectTests;

public class TestForClientService
{
    private readonly Mock<IClientRepository> _clientRepositoryMock;
    private readonly ClientService _clientService;

    public TestForClientService()
    {
        _clientRepositoryMock = new Mock<IClientRepository>();
        _clientService = new ClientService(_clientRepositoryMock.Object);
    }

    [Fact]
    public async Task AddClientPhysicalToBase_Should_Add_ClientPhysical_To_Base()
    {
        AddClientPhysicalDTO clientPhysical = new AddClientPhysicalDTO()
        {
            Name = "John",
            Surname = "Stonson",
            Address = "Paris",
            Email = "drStone@gmial.com",
            TelephoneNumber = "123456789",
            PESEL = "12309893847",
        };
      
        await _clientService.AddClientPhysicalToBase(clientPhysical);

        _clientRepositoryMock.Verify(repo => repo.AddClientPhysicalToBase(It.IsAny<ClientPhysical>()), Times.Once);
    }
    [Fact]
    public async Task AddClientPhysicalToBase_Should_Return_NotCorrectLengthPesel()
    {
        AddClientPhysicalDTO clientPhysical = new AddClientPhysicalDTO()
        {
            Name = "John",
            Surname = "Stonson",
            Address = "Paris",
            Email = "drStone@gmial.com",
            TelephoneNumber = "123456789",
            PESEL = "23",
        };
      
        await Assert.ThrowsAsync<NotCorrectLengthPesel>(() => _clientService.AddClientPhysicalToBase(clientPhysical));

        _clientRepositoryMock.Verify(repo => repo.AddClientPhysicalToBase(It.IsAny<ClientPhysical>()), Times.Never);
    }
    [Fact]
    public async Task AddClientPhysicalToBase_Should_Return_NotCorrectFormatPesel()
    {
        AddClientPhysicalDTO clientPhysical = new AddClientPhysicalDTO()
        {
            Name = "John",
            Surname = "Stonson",
            Address = "Paris",
            Email = "drStone@gmial.com",
            TelephoneNumber = "123456789",
            PESEL = "1234567890q",
        };
      
        await Assert.ThrowsAsync<NotCorrectFormatPesel>(() => _clientService.AddClientPhysicalToBase(clientPhysical));

        _clientRepositoryMock.Verify(repo => repo.AddClientPhysicalToBase(It.IsAny<ClientPhysical>()), Times.Never);
    }
    [Fact]
    public async Task AddClientFirmToBase_With9KRS_Should_Add_ClientPhysical_To_Base()
    {
        AddClientFirmDTO clientFirm = new AddClientFirmDTO()
        {
            Name = "John",
            Address = "Paris",
            Email = "drStone@gmial.com",
            TelephoneNumber = "123456789",
            KRS = "123456789",
        };
     
      
        await _clientService.AddClientFirmToBase(clientFirm);


        _clientRepositoryMock.Verify(repo => repo.AddClientFirmToBase(It.IsAny<ClientFirm>()), Times.Once);
    }
    [Fact]
    public async Task AddClientFirmToBase_With14KRS_Should_Add_ClientPhysical_To_Base()
    {
        AddClientFirmDTO clientFirm = new AddClientFirmDTO()
        {
            Name = "A",
            Address = "City",
            Email = "aCity@gmial.com",
            TelephoneNumber = "123456789",
            KRS = "12345678901234",
        };
        
        await _clientService.AddClientFirmToBase(clientFirm);


        _clientRepositoryMock.Verify(repo => repo.AddClientFirmToBase(It.IsAny<ClientFirm>()), Times.Once);
    }
    [Fact]
    public async Task AddClientFirmToBase_With14KRS_Should_Return_NotCorrectFormatKRS()
    {
        AddClientFirmDTO clientFirm = new AddClientFirmDTO()
        {
            Name = "A",
            Address = "City",
            Email = "aCity@gmial.com",
            TelephoneNumber = "123456789",
            KRS = "12345678q",
        };
        
        await Assert.ThrowsAsync<NotCorrectFormatKRS>(() => _clientService.AddClientFirmToBase(clientFirm));

        _clientRepositoryMock.Verify(repo => repo.AddClientFirmToBase(It.IsAny<ClientFirm>()), Times.Never);
    }
    [Fact]
    public async Task AddClientFirmToBase_With14KRS_Should_Return_NotCorrectLengthKRS()
    {
        AddClientFirmDTO clientFirm = new AddClientFirmDTO()
        {
            Name = "A",
            Address = "City",
            Email = "aCity@gmial.com",
            TelephoneNumber = "123456789",
            KRS = "1",
        };
        
        await Assert.ThrowsAsync<NotCorrectLengthKRS>(() => _clientService.AddClientFirmToBase(clientFirm));

        _clientRepositoryMock.Verify(repo => repo.AddClientFirmToBase(It.IsAny<ClientFirm>()), Times.Never);
    }
    [Fact]
    public async Task UpdateClientPhysicalInBase_Should_Return_ClientNotFound()
    {
        int clientId = 1;
        UpdateClientPhysicalDTO clientPhysicalDto = new UpdateClientPhysicalDTO
        {
            Name = "John",
            Surname = "Stonson",
            Address = "Paris",
            Email = "drStone@gmail.com",
            TelephoneNumber = "123456789",
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientPhysicalFromBase(clientId))
            .ReturnsAsync((ClientPhysical?)null);

        await Assert.ThrowsAsync<ClientNotFound>(() => _clientService.UpdateClientPhysicalInBase(clientId, clientPhysicalDto));
    }
    [Fact]
    public async Task UpdateClientPhysicalInBase_Should_Update_ClientPhysical_In_Base()
    {
        int clientId = 1;
        ClientPhysical existingClient = new ClientPhysical
        {
            Id = clientId,
            Name = "name",
            Surname = "surname",
            Address = "address",
            Email = "email.@gmail.com",
            TelephoneNumber = "000000000",
            PESEL = "00000000000"
        };

        UpdateClientPhysicalDTO clientPhysicalDto = new UpdateClientPhysicalDTO
        {
            Name = "John",
            Surname = "Stonson",
            Address = "Paris",
            Email = "drStone@gmail.com",
            TelephoneNumber = "123456789",
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientPhysicalFromBase(clientId))
            .ReturnsAsync(existingClient);

        _clientRepositoryMock.Setup(repo => repo.UpdateClientPhysicalInBase(existingClient, clientPhysicalDto))
            .Returns(Task.CompletedTask);

        await _clientService.UpdateClientPhysicalInBase(clientId, clientPhysicalDto);

        _clientRepositoryMock.Verify(repo => repo.UpdateClientPhysicalInBase(existingClient, clientPhysicalDto), Times.Once);
    }
    [Fact]
    public async Task UpdateClientFirmInBase_Should_Return_ClientNotFound()
    {
        int clientId = 1;
        UpdateClientFirmDTO clientFirm = new UpdateClientFirmDTO
        {
            Name = "A",
            Address = "City",
            Email = "aCity@gmial.com",
            TelephoneNumber = "123456789",
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientFirmFromBase(clientId))
            .ReturnsAsync((ClientFirm?)null);

        await Assert.ThrowsAsync<ClientNotFound>(() => _clientService.UpdateClientFirmInBase(clientId, clientFirm));
    }
    [Fact]
    public async Task UpdateClientFirmInBase_Should_Update_ClientFirm_In_Base()
    {
        int clientId = 1;
        ClientFirm existingClient = new ClientFirm()
        {
            Id = clientId,
            Name = "name",
            Address = "address",
            Email = "email.@gmail.com",
            TelephoneNumber = "000000000",
            KRS = "00000000000000"
        };

        UpdateClientFirmDTO newClientFirm = new UpdateClientFirmDTO
        {
            Name = "John",
            Address = "Paris",
            Email = "drStone@gmail.com",
            TelephoneNumber = "123456789",
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientFirmFromBase(clientId))
            .ReturnsAsync(existingClient);

        _clientRepositoryMock.Setup(repo => repo.UpdateClientFirmInBase(existingClient, newClientFirm))
            .Returns(Task.CompletedTask);

        await _clientService.UpdateClientFirmInBase(clientId, newClientFirm);

        _clientRepositoryMock.Verify(repo => repo.UpdateClientFirmInBase(existingClient, newClientFirm), Times.Once);
    }

    [Fact]
    public async Task DeleteClientPhysicalFromBase_Should_Return_ClientNotFound()
    {
        int clientId = 1;
       
        _clientRepositoryMock.Setup(repo => repo.GetClientPhysicalFromBase(clientId))
            .ReturnsAsync((ClientPhysical?)null);

        await Assert.ThrowsAsync<ClientNotFound>(() => _clientService.DeleteClientPhysicalFromBase(clientId));

    }
    
    [Fact]
    public async Task DeleteClientPhysicalFromBase_Should_Delete_ClientPhysical_From_Base()
    {
        int clientId = 1;
        ClientPhysical clientPhysical = new ClientPhysical()
        {
            Id = clientId,
            Name = "name",
            Surname = "surname",
            Address = "address",
            Email = "email.@gmail.com",
            TelephoneNumber = "000000000",
            PESEL = "00000000000"
        };

        _clientRepositoryMock.Setup(repo => repo.GetClientPhysicalFromBase(clientId))
            .ReturnsAsync(clientPhysical);

        _clientRepositoryMock.Setup(repo => repo.DeleteClientPhysicalFromBase(clientPhysical))
            .Returns(Task.CompletedTask);

        await _clientService.DeleteClientPhysicalFromBase(clientId);

        _clientRepositoryMock.Verify(repo => repo.DeleteClientPhysicalFromBase(clientPhysical), Times.Once);

    }


}