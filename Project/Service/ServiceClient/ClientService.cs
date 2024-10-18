using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;

namespace Project.Service;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    public async Task AddClientPhysicalToBase(AddClientPhysicalDTO clientPhysicalDto)
    {
        CheckCorrectFormatPesel(clientPhysicalDto.PESEL);
        CheckLengthPesel(clientPhysicalDto.PESEL);
        
        ClientPhysical clientPhysical = new ClientPhysical()
        {
            Name = clientPhysicalDto.Name,
            Surname = clientPhysicalDto.Surname,
            Address = clientPhysicalDto.Address,
            Email = clientPhysicalDto.Email,
            TelephoneNumber = clientPhysicalDto.TelephoneNumber,
            PESEL = clientPhysicalDto.PESEL,
            IsDeleted = false
        };
        await clientRepository.AddClientPhysicalToBase(clientPhysical);
    }
    public async Task AddClientFirmToBase(AddClientFirmDTO clientFirmDto)
    {
        CheckCorrectFormatKRS(clientFirmDto.KRS);
        CheckLengthKRS(clientFirmDto.KRS);
        
        ClientFirm clientFirm = new ClientFirm()
        {
            Name = clientFirmDto.Name,
            Address = clientFirmDto.Address,
            Email = clientFirmDto.Email,
            TelephoneNumber = clientFirmDto.TelephoneNumber,
            KRS = clientFirmDto.KRS
        };
        await clientRepository.AddClientFirmToBase(clientFirm);
    }

    private void CheckLengthPesel(string pesel)
    {
        if (pesel.Length != 11)
        {
            throw new NotCorrectLengthPesel();
        }
    }

    private void CheckCorrectFormatPesel(string pesel)
    {
        if (!pesel.All(char.IsDigit))
        {
            throw new NotCorrectFormatPesel();
        }
    }
    
    private void CheckLengthKRS(string krs)
    {
        if (krs.Length != 9 && krs.Length != 14)
        {
            throw new NotCorrectLengthKRS();
        }
    }

    private void CheckCorrectFormatKRS(string krs)
    {
        if (!krs.All(char.IsDigit))
        {
            throw new NotCorrectFormatKRS();
        }
    }

    public async Task UpdateClientPhysicalInBase(int id, UpdateClientPhysicalDTO clientPhysicalDto)
    {
        ClientPhysical? clientPhysical = await clientRepository.GetClientPhysicalFromBase(id);
        if (clientPhysical == null)
        {
            throw new ClientNotFound();
        }
        await clientRepository.UpdateClientPhysicalInBase(clientPhysical, clientPhysicalDto);
    }

    public async Task UpdateClientFirmInBase(int id, UpdateClientFirmDTO clientFirmDto)
    {
        ClientFirm? clientFirm = await clientRepository.GetClientFirmFromBase(id);
        if (clientFirm == null)
        {
            throw new ClientNotFound();
        }
        await clientRepository.UpdateClientFirmInBase(clientFirm, clientFirmDto);
    }

    public async Task DeleteClientPhysicalFromBase(int id)
    {
        ClientPhysical? clientPhysical = await clientRepository.GetClientPhysicalFromBase(id);
        if (clientPhysical == null)
        {
            throw new ClientNotFound();
        } 
        await clientRepository.DeleteClientPhysicalFromBase(clientPhysical);
    }
} 