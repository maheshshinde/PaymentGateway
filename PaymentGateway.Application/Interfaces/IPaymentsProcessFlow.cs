using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Interfaces
{
    public interface IPaymentsProcessFlow
    {
        public Task<ProcessPaymentResponse> PaymentsProcess(ProcessPaymentsCommand request);
    }
}
