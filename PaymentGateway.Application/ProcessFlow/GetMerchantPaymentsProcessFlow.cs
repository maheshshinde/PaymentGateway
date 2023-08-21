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

        public async Task<GetMerchantPaymentsResponse> GetMerchantPaymentDetailProcess(GetMerchantPaymentsQuery request)
        {
            GetMerchantPaymentsResponse merchantPaymentsResponse = new();

            var payments = await _paymentQueryRepository.GetMerchantPaymentDetails(request.MerchantId, request.PaymentId);

            if (!payments.Any())
            {
                merchantPaymentsResponse.ErrorMessage = $"[ Application.GetMerchantPaymentDetailProcess Payment details not found for merchant {request.MerchantId} and Payment {request.PaymentId}]";
                return merchantPaymentsResponse;
            }

            merchantPaymentsResponse.Payments = payments.PaymentsToPaymentsDto().ToList();

            return merchantPaymentsResponse;
        }
    }
}
