using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.DTO;
using Project.Model;

namespace Project.Repository;

public class ClientRepository(APBDContext apbdContext) : IClientRepository
{

    public async Task AddClientPhysicalToBase(ClientPhysical clientPhysical)
    {
        await apbdContext.ClientPhysicals.AddAsync(clientPhysical);
        await apbdContext.SaveChangesAsync();
    }
    public async Task AddClientFirmToBase(ClientFirm clientFirm)
    {
        await apbdContext.ClientFirms.AddAsync(clientFirm);
        await apbdContext.SaveChangesAsync();
    }

    public async Task UpdateClientPhysicalInBase(ClientPhysical clientPhysicalToChange, UpdateClientPhysicalDTO clientPhysical)
    {
        clientPhysicalToChange.Name = clientPhysical.Name;
        clientPhysicalToChange.Surname = clientPhysical.Surname;
        clientPhysicalToChange.Address = clientPhysical.Address;
        clientPhysicalToChange.TelephoneNumber = clientPhysical.TelephoneNumber;
        clientPhysicalToChange.Email = clientPhysical.Email;

        await apbdContext.SaveChangesAsync();

    }

    public async Task UpdateClientFirmInBase(ClientFirm clientFirmToChange, UpdateClientFirmDTO clientFirm)
    {
        clientFirmToChange.Name = clientFirm.Name;
        clientFirmToChange.Address = clientFirm.Address;
        clientFirmToChange.Email = clientFirm.Email;
        clientFirmToChange.TelephoneNumber = clientFirm.TelephoneNumber;

        await apbdContext.SaveChangesAsync();
    }

    public async Task<ClientPhysical?> GetClientPhysicalFromBase(int id)
    {
        return apbdContext.ClientPhysicals.SingleOrDefault(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<ClientFirm?> GetClientFirmFromBase(int id)
    {
        return apbdContext.ClientFirms.SingleOrDefault(x => x.Id == id)!;
    }

    public async Task DeleteClientPhysicalFromBase(ClientPhysical clientPhysical)
    {
        clientPhysical.Name = "";
        clientPhysical.Surname = "";
        clientPhysical.Address = "";
        clientPhysical.Email = "";
        clientPhysical.TelephoneNumber = "";
        clientPhysical.PESEL = "";
        clientPhysical.IsDeleted = true;
        await apbdContext.SaveChangesAsync();
    }
    
    public async Task<AbstractClient?> GetClient(int idClient)
    {
        return await apbdContext.AbstractClients.Include(x=>x.Contracts).Include(x=>x.Subscriptions).SingleOrDefaultAsync(x => x.Id == idClient);
    }
}