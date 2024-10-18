using Project.DTO;
using Project.Model;

namespace Project.Repository;

public interface IClientRepository
{
    public Task AddClientPhysicalToBase(ClientPhysical clientPhysical);
    public Task AddClientFirmToBase(ClientFirm clientFirm);
    public Task UpdateClientPhysicalInBase(ClientPhysical clientPhysicalToChange, UpdateClientPhysicalDTO clientPhysical);
    public Task UpdateClientFirmInBase(ClientFirm clientFirmToChange, UpdateClientFirmDTO clientFirm);
    public Task<ClientPhysical?> GetClientPhysicalFromBase(int id);
    public Task<ClientFirm?> GetClientFirmFromBase(int id);
    public Task DeleteClientPhysicalFromBase(ClientPhysical clientPhysical);
    public Task<AbstractClient?> GetClient(int idClient);


}