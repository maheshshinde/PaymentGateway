using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.ProcessFlow;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Tests.UnitTests.Application
{
    [TestFixture]
    public class BuildPaymentsModelUnitTests
    {
        private Mock<IMaskingService> _maskingService;
        private IBuildPaymentsModel _buildPaymentsModel;
        private ProcessPaymentsCommand request;
        private BankResponse bankResponse;

        [SetUp]
        public void Setup()
        {
            _maskingService = new Mock<IMaskingService>();
            _buildPaymentsModel = new BuildPaymentsModel(_maskingService.Object);

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

            bankResponse = new BankResponse()
            {
                PaymentStatus = PaymentStatus.Accepted,
                ErrorMessage = string.Empty
            };
        }

        [Test]
        public void Given_BuildPayments_ValidInputs_Return_Payments_Model()
        {
            _maskingService.Setup(x => x.MaskCardHolderName("Xyz ABC").Result).Returns("Xyz ***");

            _maskingService.Setup(x => x.MaskCardNumber("1111 1111 1111 1111").Result).Returns("************1111");

            var result = _buildPaymentsModel.CreatePaymentModel(request, bankResponse).Result;

            result.CardHolderName.Should().NotBeNullOrWhiteSpace();
            result.CardNumber.Should().NotBeNullOrWhiteSpace();
            result.Amount.Should().Be(100);
            result.ExpiryMonth.Should().NotBeNullOrWhiteSpace();
            result.ExpiryYear.Should().NotBeNullOrWhiteSpace();
            result.MerchantId.Should().Be(Guid.Parse("f5e13233-7812-448b-80d1-1d1a7a46993e"));
        }
    }
}
