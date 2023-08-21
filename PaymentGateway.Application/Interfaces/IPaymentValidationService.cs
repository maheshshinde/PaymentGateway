using PaymentGateway.Application.Commands;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentValidationService
    {
        public Task<IEnumerable<string>> ValidateData(ProcessPaymentsCommand payment);
    }
}
