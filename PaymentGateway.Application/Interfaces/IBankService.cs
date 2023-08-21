using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Interfaces
{
    public interface IBankService
    {
        public Task<BankResponse> MakePayment(ProcessPaymentsCommand processPayments);
    }
}
