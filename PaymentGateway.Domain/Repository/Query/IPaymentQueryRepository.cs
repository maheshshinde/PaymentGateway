using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repository.Query.Base;

namespace PaymentGateway.Domain.Repository.Query
{
    public interface IPaymentQueryRepository : IQueryRepository<Payment>
    {
        public Task<IEnumerable<Payment>> GetMerchantPayments(Guid merchantId);
    }
}
