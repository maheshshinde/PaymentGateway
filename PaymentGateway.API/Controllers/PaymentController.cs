using MediatR;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Queries;

namespace PaymentGateway.API.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IMediator _mediator;

        public PaymentController(ILogger<PaymentController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("processpayment")]
        public async Task<ActionResult> ProcessPayment([FromBody] ProcessPaymentsCommand command)
        {
            _logger.LogInformation($"API Process Payment {command.MerchantId}");

            var response = await _mediator.Send(command);

            if (response.IsSuccess)
            {
                return StatusCode(201, response.PaymentSuccessResponse);
            }

            return StatusCode(400, response.PaymentFailureResponse);
        }

        [HttpGet("merchantpayments/{MerchantId}")]
        public async Task<ActionResult> GetMerchantPayments([FromRoute] GetMerchantPaymentsQuery query)
        {
            _logger.LogInformation($"API Get Merchant Payments {query.MerchantId}");

            var result = await _mediator.Send(query);

            if (result.Payments.Any())
            {
                return StatusCode(200, result.Payments);
            }

            return StatusCode(204, result.ErrorMessage);
        }
    }
}
