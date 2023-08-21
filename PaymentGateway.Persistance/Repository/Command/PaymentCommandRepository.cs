using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repository.Command;
using PaymentGateway.Persistance.Repository.Command.Base;

namespace PaymentGateway.Persistance.Repository.Command
{
    public class PaymentCommandRepository : CommandRepository<Payment>, IPaymentCommandRepository
    {
        public PaymentCommandRepository(PaymentDbContext context) : base(context)
        {
        }
    }
}
