using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Entities;

namespace PaymentGateway.Application.ProcessFlow
{
    public class BuildPaymentsModel : IBuildPaymentsModel
    {
        private readonly IMaskingService _maskingService;

        public BuildPaymentsModel(IMaskingService maskingService)
        {
            _maskingService = maskingService;
        }

        public async Task<Payment> CreateModel(ProcessPaymentsCommand request, BankResponse bankResponse)
        {
            var payment = new Payment
            {
                Amount = request.Amount,
                Currency = request.Currency,
                MerchantId = request.MerchantId,
                CVV = request.CVV,
                ExpiryMonth = request.ExpiryMonth,
                ExpiryYear = request.ExpiryYear,

                PaymentStatus = bankResponse.PaymentStatus,
                PaymentRejectedReason = bankResponse.RejectedReason,

                // *** Mask Card Number
                CardNumber = await _maskingService.MaskCardNumber(request.CardNumber),

                // *** Mask Card Holder Name
                CardHolderName = await _maskingService.MaskCardHolderName(request.CardHolderName)
            };

            return await Task.FromResult(payment);
        }
    }
}
