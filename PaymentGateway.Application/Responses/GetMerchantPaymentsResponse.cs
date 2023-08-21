using PaymentGateway.Application.DTO;

namespace PaymentGateway.Application.Responses
{
    public class GetMerchantPaymentsResponse
    {
        public GetMerchantPaymentsResponse()
        {
            this.Payments = new List<PaymentDto>();
        }

        public IEnumerable<PaymentDto> Payments { get; set; }

        public string ErrorMessage { get; set; }
    }
}
