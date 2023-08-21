using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using PaymentGateway.Application.Queries;
using System.Net;

namespace PaymentGateway.Tests.IntegrationTests
{
    [TestFixture]
    public class GetMerchantPaymentsIntegrationTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
            _httpClient = factory.CreateClient();

            var apiKey = "Te$t@8080";
            _httpClient.DefaultRequestHeaders.Add("Api_Key", apiKey);
        }

        [Test]
        public void Get_Merchants_Payments_API_Valid_Data()
        {
            // Arrange
            GetMerchantPaymentsQuery request = new GetMerchantPaymentsQuery()
            {
                MerchantId = Guid.Parse("f5e13233-7812-448b-80d1-1d1a7a46993e"),
            };

            // Act
            var result = _httpClient.GetAsync($"/api/Payment/merchantpayments/{request.MerchantId}").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public void Get_Merchants_Payments_API_InValid_Data()
        {
            // Arrange
            GetMerchantPaymentsQuery request = new GetMerchantPaymentsQuery()
            {
                MerchantId = Guid.NewGuid(),
            };

            // Act
            var result = _httpClient.GetAsync($"/api/Payment/merchantpayments/{request.MerchantId}").Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
