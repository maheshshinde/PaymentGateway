using Microsoft.EntityFrameworkCore;
using PaymentGateway.Domain.Entities;
using PaymentGateway.Domain.Repository.Query;

namespace PaymentGateway.Persistance.Repository.Query
{
    public class PaymentQueryRepository : IPaymentQueryRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentQueryRepository(PaymentDbContext context) : base()
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetMerchantPaymentDetails(Guid merchantId, Guid paymentId)
        {
            return await _context.Payments.Where(p => p.MerchantId == merchantId && p.PaymentId == paymentId).ToListAsync();
        }
    }
}
