using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Domain.Repository.Command;
using PaymentGateway.Domain.Repository.Command.Base;
using PaymentGateway.Domain.Repository.Query;
using PaymentGateway.Domain.Repository.Query.Base;
using PaymentGateway.Persistance.Repository.Command;
using PaymentGateway.Persistance.Repository.Command.Base;
using PaymentGateway.Persistance.Repository.Query;
using PaymentGateway.Persistance.Repository.Query.Base;

namespace PaymentGateway.Persistance
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));
            services.AddTransient<IPaymentQueryRepository, PaymentQueryRepository>();
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddTransient<IPaymentCommandRepository, PaymentCommandRepository>();

            return services;
        }
    }
}
