using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Application.Handlers.CommandHandlers;
using PaymentGateway.Application.Handlers.QueryHandlers;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.ProcessFlow;
using PaymentGateway.Application.Queries;

namespace PaymentGateway.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMerchantPaymentsHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessPaymentHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMerchantPaymentsQuery).Assembly));

            services.AddScoped<IPaymentsProcessFlow, PaymentsProcessFlow>();
            services.AddScoped<IBuildPaymentsModel, BuildPaymentsModel>();
            services.AddScoped<IGetMerchantPaymentsProcessFlow, GetMerchantPaymentsProcessFlow>();

            return services;
        }
    }
}
