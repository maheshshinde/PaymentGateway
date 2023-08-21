using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Infrastructure.BankService;
using PaymentGateway.Infrastructure.DataMaskingService;
using PaymentGateway.Infrastructure.ValidationService;

namespace PaymentGateway.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBankService, MockBankService>();
            services.AddScoped<IMaskingService, MaskingService>();
            services.AddScoped<IPaymentValidationService, PaymentValidationService>();

            return services;
        }
    }
}
