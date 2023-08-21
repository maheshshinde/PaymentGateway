using MediatR;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Commands
{
    public class ProcessPaymentsCommand : IRequest<ProcessPaymentResponse>
    {
        public Guid MerchantId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }

        public string CVV { get; set; }
    }
}
