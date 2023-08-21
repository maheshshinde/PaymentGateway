using FluentAssertions;
using NUnit.Framework;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.DataMaskingService;

namespace PaymentGateway.UnitTests.Infrastructure
{
    [TestFixture]
    public class MaskingServiceUnitTests
    {
        private IMaskingService _maskingService;

        [SetUp]
        public void Setup()
        {
            _maskingService = new MaskingService();
        }

        [Test]
        public void Given_MaskCardNumber_EmptyCardNumber_Return_EmptyString()
        {
            string cardNumber = string.Empty;
            var result = _maskingService.MaskCardNumber(cardNumber).Result;

            result.Should().Be(string.Empty);
        }

        [Test]
        public void Given_MaskCardNumber_PassNull_CardNumber_Return_EmptyString()
        {
            var result = _maskingService.MaskCardNumber(null).Result;

            result.Should().Be(string.Empty);
        }

        [Test]
        public void Given_MaskCardNumber_ValidCardNumber_Return_MaskedCardNumber()
        {
            string cardNumber = "1111 1111 1111 1111";
            var result = _maskingService.MaskCardNumber(cardNumber).Result;

            result.Should().Be("***************1111");
        }

        [Test]
        public void Given_MaskCardHolderName_EmptyName_Return_EmptyString()
        {
            string cardNumber = string.Empty;
            var result = _maskingService.MaskCardHolderName(cardNumber).Result;

            result.Should().Be(string.Empty);
        }

        [Test]
        public void Given_MaskCardHolderName_PassNull_Name_Return_EmptyString()
        {
            var result = _maskingService.MaskCardHolderName(null).Result;

            result.Should().Be(string.Empty);
        }

        [Test]
        public void Given_MaskCardHolderName_ValidName_Return_MaskedFullName()
        {
            string cardNumber = "Xyz abc";
            var result = _maskingService.MaskCardHolderName(cardNumber).Result;

            result.Should().Be("Xyz ***");
        }

        [Test]
        public void Given_MaskCardHolderName_ValidFistName_Return_FirstName()
        {
            string cardNumber = "Xyz";
            var result = _maskingService.MaskCardHolderName(cardNumber).Result;

            result.Should().Be("Xyz");
        }
    }
}
