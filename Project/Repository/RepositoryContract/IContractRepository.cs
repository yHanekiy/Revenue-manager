using Project.Model;

namespace Project.Repository;

public interface IContractRepository
{
    public Task CreateContract(Contract contract);
    public Task<Software?> GetSoftwareById(int id);
    public Task<Discount?> GetBestActiveDiscountForContract();
    public Task<Contract?> GetContract(int idContract);
    public Task MakePaymentForContract(Contract contract, decimal payment);
    public Task FinishContractPayment(Contract contract, decimal payment);





}