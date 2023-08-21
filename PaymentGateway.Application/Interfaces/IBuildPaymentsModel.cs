using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Interfaces
{
    public interface IBuildPaymentsModel
    {
        public Task<Payment> CreateModel(ProcessPaymentsCommand request, BankResponse bankResponse);
    }
}
