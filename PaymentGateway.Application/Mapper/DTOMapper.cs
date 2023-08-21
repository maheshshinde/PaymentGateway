using PaymentGateway.Application.DTO;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.Mapper
{
    public static class DTOMapper
    {
        public static IEnumerable<PaymentDto> PaymentsToPaymentsDto(this IEnumerable<Payment> query)
        {
            return query.Select(p => new PaymentDto
            {
                Amount = p.Amount,
                Currency = p.Currency,
                MerchantId = p.MerchantId,
                PaymentId = p.PaymentId,
                CardHolderName = p.CardHolderName,
                CardNumber = p.CardNumber,
                CVV = p.CVV,
                ExpiryMonth = p.ExpiryMonth,
                ExpiryYear = p.ExpiryYear
            });
        }
    }
}
