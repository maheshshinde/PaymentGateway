using MediatR;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Queries
{
    public class GetMerchantPaymentsQuery : IRequest<GetMerchantPaymentsResponse>
    {
        public Guid MerchantId { get; set; }

        public Guid PaymentId { get; set; }
    }
}
