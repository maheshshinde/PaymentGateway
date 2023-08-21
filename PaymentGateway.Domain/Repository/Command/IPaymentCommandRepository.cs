using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repository.Command.Base;

namespace PaymentGateway.Domain.Repository.Command
{
    public interface IPaymentCommandRepository : ICommandRepository<Payment>
    {
    }
}
