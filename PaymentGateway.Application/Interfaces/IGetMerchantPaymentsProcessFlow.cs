using PaymentGateway.Application.Queries;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Interfaces
{
    public interface IGetMerchantPaymentsProcessFlow
    {
        public Task<GetMerchantPaymentsResponse> Process(GetMerchantPaymentsQuery request);
    }
}
