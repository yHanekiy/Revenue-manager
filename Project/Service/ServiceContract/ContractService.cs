using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;

namespace Project.Service;

public class ContractService(IContractRepository contractRepository, IClientRepository clientRepository) : IContractService
{
    public async Task CreateContract(ContractCreateDTO contractCreate)
    {
        await CheckClientInBase(contractCreate.IdClient);
        CheckCorrectFormatDateOnly(contractCreate.DateTo);
        CheckDifferenceBetweenStartAndFinishDate( DateOnly.Parse(contractCreate.DateTo));
        CheckLimitYearToSupport(contractCreate.YearsToSupport);
        CheckLimitYearToBuy(contractCreate.YearsToBuy);
        Software? software = await contractRepository.GetSoftwareById(contractCreate.IdSoftware);
        if (software == null)
        {
            throw new SoftwareIsNotFound();
        }
        await CheckAlreadyHaveUnpaidContractWithThisProduct(contractCreate);

        
        Contract contract = new Contract()
        {
            IdSoftware = contractCreate.IdSoftware,
            IdClient = contractCreate.IdClient,
            DateFrom = DateOnly.FromDateTime(DateTime.Now),
            DateTo = DateOnly.Parse(contractCreate.DateTo),
            YearsToBuy = contractCreate.YearsToBuy,
            YearsToSupport = contractCreate.YearsToSupport,
            AlreadyPaid = 0,
            Price = await CalculatePrice(contractCreate, software),
            Desciption = contractCreate.Description,
            IsAlreadyPaid = false
        };
        await contractRepository.CreateContract(contract);
    }

    public async Task MakePaymentForContract(int id, PaymentDTO payment)
    {
        Contract? contract = await contractRepository.GetContract(id);
        if (contract == null)
        {
            throw new ContractIsNotFound();
        }

        await CheckClientInBase(payment.IdClient);
        CheckContractsBelongsToClient(contract,payment.IdClient);
        CheckContractIsAlreadyPaid(contract);
        CheckDataLimit(contract);
        CheckPaymentIsPositiveValue(payment.Payment);

        if (contract.AlreadyPaid + payment.Payment <= contract.Price)
        {
            await contractRepository.MakePaymentForContract(contract, payment.Payment);
        }
        else
        {
            payment.Payment = contract.Price - contract.AlreadyPaid;
            await contractRepository.FinishContractPayment(contract, payment.Payment);
        }
    }

    private void CheckContractsBelongsToClient(Contract contract, int idClient)
    {
        if (contract.IdClient != idClient)
        {
            throw new NotOwnerOfContract();
        }
    }

    private void CheckDataLimit(Contract contract)
    {
        if (contract.DateTo<DateOnly.FromDateTime(DateTime.Now))
        {
            throw new TimeToPayExpired();
        }
    }

    private void CheckPaymentIsPositiveValue(decimal payment)
    {
        if (payment<=0)
        {
            throw new NotPositiveValueForPayment();
        }
    }

    private void CheckContractIsAlreadyPaid(Contract contract)
    {
        if (contract.IsAlreadyPaid)
        {
            throw new ContractIsAlreadyPaid();
        }
    }

    private async Task CheckClientInBase(int idClient)
    {
        AbstractClient? client = await clientRepository.GetClient(idClient);
        if (client == null)
        {
            throw new ClientNotFound();
        }

        if (client is ClientPhysical clientPhysical)
        {
            if (clientPhysical.IsDeleted)
            {
                throw new ClientNotFound();
            }
        }
    }

    private async Task<decimal> CalculatePrice(ContractCreateDTO contractCreateDto, Software software)
    {
        Discount? discount = await contractRepository.GetBestActiveDiscountForContract();
        AbstractClient? client = await clientRepository.GetClient(contractCreateDto.IdClient);
        decimal valueDiscount = client!.Contracts.Any(x=>x.IsAlreadyPaid) ? 5m: client!.Subscriptions.Count >0? 5m:0m;
        if (discount!=null)
        {
            valueDiscount = (discount.Value+valueDiscount) / 100;
        }
        decimal fullPrice = software.PurchasePricePerYear * contractCreateDto.YearsToBuy +
                           1000 * contractCreateDto.YearsToSupport;
        return fullPrice-fullPrice*valueDiscount;
    }

    private void CheckDifferenceBetweenStartAndFinishDate(DateOnly dateTo)
    {
        if (dateTo.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber < 3)
        {
            throw new NotEnoughTimeContract();
        }
        if (dateTo.DayNumber - DateOnly.FromDateTime(DateTime.Now).DayNumber > 30)
        {
            throw new TooMuchTimeContract();
        }
    }

    private void CheckCorrectFormatDateOnly(string date)
    {
        if (!DateOnly.TryParse(date, out DateOnly date1))
        {
            throw new NotCorrectDataFormat();
        }
    }

    private void CheckLimitYearToSupport(int yearToSupport)
    {
        if (yearToSupport  < 0 || yearToSupport >3)
        {
            throw new NotPossibleSupport();
        }
    }
    private void CheckLimitYearToBuy(int yearToBuy)
    {
        if (yearToBuy  < 1)
        {
            throw new NotPossibleBuy();
        }
    }

    private async Task CheckAlreadyHaveUnpaidContractWithThisProduct(ContractCreateDTO contractCreateDto)
    {
        AbstractClient? client = await clientRepository.GetClient(contractCreateDto.IdClient);
        
        if (client!.Contracts.Any(x =>
                x.IdSoftware == contractCreateDto.IdSoftware && x.DateTo >= DateOnly.FromDateTime(DateTime.Now)))
            {
                throw new AlreadyHaveUnpaidContractWithThisProduct();
            }
    }
}