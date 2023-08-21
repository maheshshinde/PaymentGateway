using FluentAssertions;
using Moq;
using NUnit.Framework;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.ProcessFlow;
using PaymentGateway.Application.Queries;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repository.Query;

namespace PaymentGateway.Tests.UnitTests.Application
{
    // *** ToDo : Required Tests implementation
    [TestFixture]
    public class GetMerchantPaymentsProcessFlowUnitTests
    {
        private Mock<IPaymentQueryRepository> _paymentRepo;
        private IGetMerchantPaymentsProcessFlow _merchantPayments;

        [SetUp]
        public void Setup()
        {
            _paymentRepo = new Mock<IPaymentQueryRepository>();

            IEnumerable<Payment> payments = new List<Payment>();

            _merchantPayments = new GetMerchantPaymentsProcessFlow(_paymentRepo.Object);

            _paymentRepo.Setup(x => x.GetMerchantPayments(Guid.NewGuid()).Result).Returns(payments);
        }

        [Test]
        public void Given_Valid_MerchantId_Get_Merchant_Payments()
        {
            GetMerchantPaymentsQuery request = new GetMerchantPaymentsQuery()
            {
                MerchantId = Guid.NewGuid(),
            };

            var result = _merchantPayments.Process(request);

            result.Should().NotBeNull();
        }
    }
}
