using Project.Error;
using Project.Model;

namespace Project.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ClientNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Client is not found");
        }
        catch (SubscriptionIsNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Subscription is not found");

        }
        catch (UserIsNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("User is not found");
        }
        catch (SoftwareIsNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Software is not found");
        }
        catch (ContractIsNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Contract is not found");
        }
        catch (CurrencyIsNotFound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Currency is not found");
        }
        catch (NotCorrectLengthPesel)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Pesel length must be 11 characters");
        }
        catch (NotCorrectFormatPesel)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Pesel is required to include only numbers");
        }
        catch (NotCorrectLengthKRS)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("KRS length must be 9 or 14 characters");
        }
        catch (NotFullPriceForMonth)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(
                "Payment must be equal to price for month subscription. All discounts will be counted next");
        }
        catch (NotCorrectFormatKRS)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("KRS is required to include only numbers");
        }
        catch (TimeToPayExpired)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Time to pay has expired");

        }
        catch (NotCorrectRenewalPeriod)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(
                "Renewal period must last for a minimum 1 month and a maximum of 24");

        }
        catch (NotOwnerOfContract)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Contract doesn`t belongs to this client");
        }
        catch (NotOwnerOfSubscription)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Subscription doesn`t belongs to this client");
        }
        catch (NotEnoughTimeContract)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(
                "The minimum number of days from start to end contract should be 3 days");
        }
        catch (ErrorConnectToExchangeApi)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Problem with connection to Exchange API");
        }
        catch (EndOfSubscription)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Subscription ends");

        }
        catch (TooMuchTimeContract)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(
                "The maximum number of days from start to end contract should be 30 days");

        }
        catch (PaymentAlreadyWasInThisPeriod)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Payment already was made in this month");

        }
        catch (NotCorrectDataFormat)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Data format is incorrect. Try\"yyyy-mm-dd\"");

        }
        catch (ErrorConvertJSONConversionRate)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Failed to parse conversion_rate from JSON or it is not a number");

        }
        catch (ContractIsAlreadyPaid)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Contract is already paid");
        }
        catch (NotPossibleSupport)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Year of support can only be for 0, 1, 2 or 3 years");
        }
        catch (NotPossibleBuy)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Impossible to buy a product for less than 1 year");

        }
        catch (NotPositiveValueForPayment)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Payment must be bigger than 0");
        }
        catch (NotPositiveValueForPrice)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Price must be bigger than 0");

        }
        catch (AlreadyHaveUnpaidContractWithThisProduct)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Already have unpaid active contract with this product");
        }
        catch (ErrorStringConvertToNumber)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Error converting string to number");

        }
        catch (Unauthorized)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Unauthorized");

        }
        /*catch (Exception)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync("Unhandled error occured");
        }*/
    }
}