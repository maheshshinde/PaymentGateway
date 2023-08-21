using PaymentGateway.Domain.Entities.Base;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Payment()
        {
            this.PaymentId = Guid.NewGuid();
        }

        public Guid PaymentId { get; set; }

        public Guid MerchantId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string CardHolderName { get; set; }

        public string CardNumber { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }

        public string CVV { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentRejectedReason PaymentRejectedReason { get; set; }
    }
}
