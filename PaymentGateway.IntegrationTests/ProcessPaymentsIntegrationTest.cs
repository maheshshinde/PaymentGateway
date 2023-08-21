using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace PaymentGateway.IntegrationTests
{
    [TestFixture]
    public class ProcessPaymentsIntegrationTest
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateDefaultClient();
        }

        [Test]
        public async Task DefaultRoute_ReturnsHelloWorld()
        {
            //var response = await _httpClient.PostAsync("",);
            //var stringResult = await response.Content.ReadAsStringAsync();
            //Assert.AreEqual("Hello World!", stringResult);
        }
    }
}
