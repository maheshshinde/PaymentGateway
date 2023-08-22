using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using PaymentGateway.Application.Commands;
using System.Net;
using System.Net.Http.Json;

namespace PaymentGateway.IntegrationTests
{
    [TestFixture]
    public class ProcessPaymentsIntegrationTest
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
        public void Process_Payments_API_Test_With_Valid_Data()
        {
            // Arrange
            var payload = new ProcessPaymentsCommand()
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

            // Act
            var result = _httpClient.PostAsJsonAsync("/api/Payment/processpayment", payload).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
        }

        [Test]
        public void Process_Payments_API_Test_With_Valid_BadData()
        {
            // Arrange
            var payload = new ProcessPaymentsCommand()
            {
                Amount = 10000,
                CardHolderName = "",
                CardNumber = "1111 1111",
                Currency = "GBP",
                CVV = "123",
                ExpiryMonth = "12",
                ExpiryYear = "2038",
                MerchantId = Guid.Parse("f5e13233-7812-448b-80d1-1d1a7a46993e"),
            };

            // Act
            var result = _httpClient.PostAsJsonAsync("/api/Payment/processpayment", payload).Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
