using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Handlers.CommandHandlers;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Repository.Command;

namespace PaymentGateway.Tests.UnitTests.Application
{
    // *** ToDo : Required Tests implementation
    [TestFixture]
    public class PaymentsProcessFlowUnitTests
    {
        private Mock<IPaymentValidationService> _validationService;
        private Mock<IBankService> _bankService;
        private Mock<IPaymentCommandRepository> _paymentCommand;
        private Mock<ILogger<ProcessPaymentHandler>> _logger;
        private Mock<IBuildPaymentsModel> _buildPaymentsModel;

        [SetUp]
        public void Setup()
        {
            _validationService = new Mock<IPaymentValidationService>();
            _bankService = new Mock<IBankService>();
            _paymentCommand = new Mock<IPaymentCommandRepository>();
            _logger = new Mock<ILogger<ProcessPaymentHandler>>();
            _buildPaymentsModel = new Mock<IBuildPaymentsModel>();

            ProcessPaymentsCommand processPaymentsCommand = new ProcessPaymentsCommand();

            _validationService.Setup(x => x.ValidateData(processPaymentsCommand).Result).Returns(new List<string> { });
        }
    }
}
