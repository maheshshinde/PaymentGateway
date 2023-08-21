using FluentAssertions;
using NUnit.Framework;
using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Domain.Enums;
using PaymentGateway.Infrastructure.BankService;

namespace PaymentGateway.UnitTests.Infrastructure
{
    [TestFixture]
    public class MockBankServiceUniTests
    {
        private IBankService _bankService;
        private ProcessPaymentsCommand processPayments;

        [SetUp]
        public void Setup()
        {
            _bankService = new MockBankService();

            processPayments = new ProcessPaymentsCommand()
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
        public void Given_ProcessPayments_PassValidValues_Bank_Returns_AccpetedStatus()
        {
            var result = _bankService.MakePayment(processPayments).Result;

            result.PaymentStatus.Should().Be(PaymentStatus.Accepted);

        }

        [Test]
        public void Given_ProcessPayments_PassInValidValues_Bank_Returns_DeclinedStatus()
        {
            processPayments.Amount = 100000;

            var result = _bankService.MakePayment(processPayments).Result;

            result.PaymentStatus.Should().Be(PaymentStatus.Declined);
        }
    }
}
