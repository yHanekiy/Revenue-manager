using System.Text.Json;
using Project.DTO;
using Project.Error;
using Project.Model;
using Project.Repository;


namespace Project.Service;

public class RevenueService(IRevenueRepository revenueRepository, IContractRepository contractRepository) : IRevenueService
{
    public async Task<RevenueDTO> RevenueCalculation(int idObject, string? currency)
    {
        if (currency == null)
        {
            currency = "pln";
        }
        string? exchangeRate = await GetExchangeRate(currency);
        if (exchangeRate == null)
        {
            throw new CurrencyIsNotFound();
        }
        CheckParseStringToNumber(exchangeRate);
        if (idObject == 0)
        {
            decimal? revenueForContracts = await revenueRepository.RevenueCalculationContracts();
            decimal? revenueForSubscriptions = await revenueRepository.RevenueCalculationSubscriptions();
            return new RevenueDTO()
            {
                ObjectName = "Company ABC",
                Revenue = (revenueForSubscriptions + revenueForContracts)*Decimal.Parse(exchangeRate)
            };
        }
        else
        {
            Software? software = await contractRepository.GetSoftwareById(idObject);
            if (software==null)
            {
                throw new SoftwareIsNotFound();
            }
            decimal? revenueForContracts = await revenueRepository.RevenueCalculationContractsForProduct(software.Id);
            decimal? revenueForSubscriptions = await revenueRepository.RevenueCalculationSubscriptionsForProduct(software.Id);

            return new RevenueDTO()
            {
                ObjectName = software.Name,
                Revenue = (revenueForSubscriptions + revenueForContracts)*Decimal.Parse(exchangeRate)
            };
        }
    }

    public async Task<RevenueDTO> PredictableRevenueCalculation(int idObject, string? currency)
    {
        if (currency == null)
        {
            currency = "pln";
        }
        string? exchangeRate = await GetExchangeRate(currency);
        if (exchangeRate == null)
        {
            throw new CurrencyIsNotFound();
        }
        CheckParseStringToNumber(exchangeRate);
        if (idObject == 0)
        {
            decimal? revenueForContracts = await revenueRepository.PredictableRevenueCalculationContracts();
            List<Subscription> subscriptionsNotCancelled =
                await revenueRepository.PredictableRevenueCalculationNotCancelledSubscriptions();
            decimal? revenueForSubscriptionsNotCancelled = subscriptionsNotCancelled.Select(x =>
                x.AlreadyPayed + x.Price * (x.RenewalPeriodInMonth - x.RealesedPayments)).Sum();
            decimal? revenueForSubscriptions = revenueForSubscriptionsNotCancelled
                                               + await revenueRepository.RevenueCalculationCancelledSubscriptions();
            return new RevenueDTO()
            {
                ObjectName = "Company ABC",
                Revenue = (revenueForSubscriptions + revenueForContracts)*Decimal.Parse(exchangeRate)
            };
        }
        else
        {
            Software? software = await contractRepository.GetSoftwareById(idObject);
            if (software==null)
            {
                throw new SoftwareIsNotFound();
            }
            decimal? revenueForContracts = await revenueRepository.PredictableRevenueCalculationContractsForProduct(software.Id);
            List < Subscription > listSubscriptionsForItem = 
                await revenueRepository.PredictableRevenueCalculationSubscriptionsForProduct(software.Id);
            decimal? predictableRevenueForSubscriptions = listSubscriptionsForItem.Select(x =>
                x.AlreadyPayed + x.Price * (x.RenewalPeriodInMonth - x.RealesedPayments)).Sum();
            decimal? revenueForSubscriptions =
                predictableRevenueForSubscriptions
                + await revenueRepository.PredictableRevenueCalculationCancelledSubscriptionsForProduct(software.Id);
            return new RevenueDTO()
            {
                ObjectName = software.Name,
                Revenue = revenueForContracts+revenueForSubscriptions*Decimal.Parse(exchangeRate)
            };
        }
    }

    private void CheckParseStringToNumber(string conversationRate)
    {
        if (!Decimal.TryParse(conversationRate, out decimal result))
        {
            throw new ErrorStringConvertToNumber();
        }
    }

    private async Task<string?> GetExchangeRate(string currency)
    {
        string url = "https://v6.exchangerate-api.com/v6/ec5d159e11a2e549ee14e567/pair/PlN/"+currency.ToUpper();

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                        var root = jsonDocument.RootElement;

                        if (root.TryGetProperty("conversion_rate", out JsonElement conversionRateElement) && conversionRateElement.ValueKind == JsonValueKind.Number)
                        {
                            string exchangeRate = conversionRateElement.GetDecimal().ToString();
                            return exchangeRate;
                        }
                        throw new ErrorConvertJSONConversionRate();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new ErrorConnectToExchangeApi();
            }
        }
    }
}