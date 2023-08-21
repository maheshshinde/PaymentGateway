using MediatR;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Queries;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Handlers.QueryHandlers
{
    public class GetMerchantPaymentsHandler : IRequestHandler<GetMerchantPaymentsQuery, GetMerchantPaymentsResponse>
    {
        private readonly IGetMerchantPaymentsProcessFlow _process;

        public GetMerchantPaymentsHandler(IGetMerchantPaymentsProcessFlow process)
        {
            _process = process;
        }

        public async Task<GetMerchantPaymentsResponse> Handle(GetMerchantPaymentsQuery request, CancellationToken cancellationToken)
        {
            return await _process.GetMerchantPaymentDetailProcess(request);
        }
    }
}
