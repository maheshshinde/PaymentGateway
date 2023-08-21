using FluentAssertions;
using NUnit.Framework;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.ValidationService;

namespace PaymentGateway.UnitTests.Infrastructure
{
    [TestFixture]
    public class PaymentValidationServiceUnitTest
    {
        private IPaymentValidationService _paymentValidationService;
        private ProcessPaymentsCommand request;

        [SetUp]
        public void Setup()
        {
            _paymentValidationService = new PaymentValidationService();

            request = new ProcessPaymentsCommand()
            {
                Amount = 100,
                CardHolderName = "Xyz ABC",
                CardNumber = "1111 1111 1111 1111",
                Currency = "GBP",
                CVV = "123",
                ExpiryMonth = "12",
                ExpiryYear = "2028",
                MerchantId = Guid.Parse("f5e13233-7812-448b-80d1-1d1a7a46993e"),
            };
        }

        [Test]
        public void Given_ProcessPaymentsCommand_PassNoValues_Validation_ReturnsErrors()
        {
            request = new ProcessPaymentsCommand();
            var result = _paymentValidationService.ValidateData(request).Result;

            result.Any().Should().BeTrue();
        }

        [Test]
        public void Given_ProcessPaymentsCommand_PassNullValue_Validation_ReturnsError()
        {
            request = null;
            var result = _paymentValidationService.ValidateData(request).Result;

            result.Any().Should().BeTrue();
        }

        [Test]
        public void Given_ProcessPaymentsCommand_PassAllValidValues_Validation_ReturnsNoErrors()
        {
            var result = _paymentValidationService.ValidateData(request).Result;

            result.Any().Should().BeFalse();
        }
    }
}
