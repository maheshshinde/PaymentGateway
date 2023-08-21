using PaymentGateway.Domain.Repository.Command.Base;

namespace PaymentGateway.Persistance.Repository.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private PaymentDbContext _context;

        public CommandRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertPaymentsAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            var result = await _context.SaveChangesAsync() > 0;

            return result;
        }
    }
}
