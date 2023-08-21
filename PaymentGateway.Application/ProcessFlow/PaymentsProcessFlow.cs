using Microsoft.Extensions.Logging;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Handlers.CommandHandlers;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Domain.Repository.Command;

namespace PaymentGateway.Application.ProcessFlow
{
    public class PaymentsProcessFlow : IPaymentsProcessFlow
    {
        private readonly IPaymentValidationService _validationService;
        private readonly IBankService _bankService;
        private readonly IPaymentCommandRepository _paymentCommand;
        private readonly ILogger<ProcessPaymentHandler> _logger;
        private readonly IBuildPaymentsModel _buildPaymentsModel;

        public PaymentsProcessFlow(IPaymentValidationService validationService, IBankService bankService,
           IPaymentCommandRepository paymentCommand, ILogger<ProcessPaymentHandler> logger, IBuildPaymentsModel buildPaymentsModel)
        {
            _validationService = validationService;
            _bankService = bankService;
            _paymentCommand = paymentCommand;
            _logger = logger;
            _buildPaymentsModel = buildPaymentsModel;
        }

        public async Task<ProcessPaymentResponse> PaymentsProcess(ProcessPaymentsCommand request)
        {
            ProcessPaymentResponse paymentResponse = new();

            try
            {
                //*** Validation service
                var validationErrors = await _validationService.ValidateData(request);

                if (validationErrors.Any())
                {
                    paymentResponse.PaymentFailureResponse.ValidationSummary = validationErrors;
                    paymentResponse.IsSuccess = false;

                    return paymentResponse;
                }

                // *** Bank Service
                var bankResponse = await _bankService.MakePayment(request);

                // *** Database update with payments
                Payment payment = await _buildPaymentsModel.CreateModel(request, bankResponse);

                paymentResponse.IsSuccess = await _paymentCommand.AddAsync(payment);
                paymentResponse.PaymentSuccessResponse.PaymentId = payment.PaymentId;

                if (bankResponse.PaymentStatus == PaymentStatus.Accepted)
                {
                    paymentResponse.PaymentSuccessResponse.SuccessMessage = $"Payment {payment.PaymentId} successfully created for merchant {payment.MerchantId}";
                }

                if (bankResponse.PaymentStatus == PaymentStatus.Declined)
                {
                    paymentResponse.IsSuccess = false;
                    string errorMessage = $"Payment declied for merchant {payment.MerchantId} by card {payment.CardNumber}, reason {payment.PaymentRejectedReason} ";
                    paymentResponse.PaymentFailureResponse.ErrorMessage = errorMessage;
                    _logger.LogError(errorMessage);
                }
            }
            catch (Exception ex)
            {
                paymentResponse.PaymentFailureResponse.ErrorMessage = ex.Message;
                paymentResponse.IsSuccess = false;
                _logger.LogError($"Error while processing the payment to {request.MerchantId} details: {ex.StackTrace}");
            }

            return paymentResponse;
        }
    }
}
