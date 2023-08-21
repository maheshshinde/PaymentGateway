using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Application.Responses
{
    public class BankResponse
    {
        public PaymentStatus PaymentStatus { get; set; }

        public PaymentRejectedReason RejectedReason { get; set; }

        public string ErrorMessage { get; set; }
    }
}
