using MediatR;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Responses;

namespace PaymentGateway.Application.Handlers.CommandHandlers
{
    public class ProcessPaymentHandler : IRequestHandler<ProcessPaymentsCommand, ProcessPaymentResponse>
    {
        private readonly IPaymentsProcessFlow _processPaymentsFlow;

        public ProcessPaymentHandler(IPaymentsProcessFlow processPaymentsFlow)
        {
            _processPaymentsFlow = processPaymentsFlow;
        }

        public async Task<ProcessPaymentResponse> Handle(ProcessPaymentsCommand request, CancellationToken cancellationToken)
        {
            return await _processPaymentsFlow.PaymentsProcess(request);
        }
    }
}
