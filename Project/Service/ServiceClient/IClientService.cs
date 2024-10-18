
using Project.DTO;
using Project.Model;

namespace Project.Service;

public interface IClientService
{
    public Task AddClientPhysicalToBase(AddClientPhysicalDTO clientPhysicalDto);
    public Task AddClientFirmToBase(AddClientFirmDTO clientFirm);
    public Task UpdateClientPhysicalInBase(int id, UpdateClientPhysicalDTO clientPhysicalDto);
    public Task UpdateClientFirmInBase(int id, UpdateClientFirmDTO clientFirmDto);
    public Task DeleteClientPhysicalFromBase(int id);
}