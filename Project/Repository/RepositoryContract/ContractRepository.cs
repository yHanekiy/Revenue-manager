using Microsoft.EntityFrameworkCore;
using Project.Context;
using Project.Model;

namespace Project.Repository;

public class ContractRepository(APBDContext apbdContext) : IContractRepository
{
    public async Task CreateContract(Contract contract)
    {
        await apbdContext.Contracts.AddAsync(contract);
        await apbdContext.SaveChangesAsync();
    }

    public async Task<Software?> GetSoftwareById(int id)
    {
        return await apbdContext.Softwares.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Discount?> GetBestActiveDiscountForContract()
    {
        return await apbdContext.Discounts.Where(x=>x.DateFrom <= DateOnly.FromDateTime(DateTime.Now) && x.DateTo >= DateOnly.FromDateTime(DateTime.Now) && (x.Offer == TypePayment.Contract || x.Offer == TypePayment.Together))
            .OrderByDescending(x => x.Value).FirstOrDefaultAsync();
    }

    public async Task<Contract?> GetContract(int idContract)
    {
        return await apbdContext.Contracts.SingleOrDefaultAsync(x => x.Id == idContract);
    }

    public async Task MakePaymentForContract(Contract contract, decimal payment)
    {
        contract.AlreadyPaid += payment;
        await apbdContext.SaveChangesAsync();
    }
    public async Task FinishContractPayment(Contract contract, decimal payment)
    {
        contract.AlreadyPaid +=  payment;
        contract.IsAlreadyPaid = true;
        await apbdContext.SaveChangesAsync();
    }
}