using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Mapper;
using PaymentGateway.Application.Queries;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Repository.Query;

namespace PaymentGateway.Application.ProcessFlow
{
    public class GetMerchantPaymentsProcessFlow : IGetMerchantPaymentsProcessFlow
    {
        private readonly IPaymentQueryRepository _paymentQueryRepository;

        public GetMerchantPaymentsProcessFlow(IPaymentQueryRepository paymentQueryRepository)
        {
            _paymentQueryRepository = paymentQueryRepository;
        }

        public async Task<GetMerchantPaymentsResponse> Process(GetMerchantPaymentsQuery request)
        {
            GetMerchantPaymentsResponse merchantPaymentsResponse = new();

            var payments = await _paymentQueryRepository.GetMerchantPayments(request.MerchantId);

            if (!payments.Any())
            {
                merchantPaymentsResponse.ErrorMessage = $"Payments not found for merchant {request.MerchantId}";
                return merchantPaymentsResponse;
            }

            merchantPaymentsResponse.Payments = payments.PaymentsToPaymentsDto().ToList();

            return merchantPaymentsResponse;
        }
    }
}
