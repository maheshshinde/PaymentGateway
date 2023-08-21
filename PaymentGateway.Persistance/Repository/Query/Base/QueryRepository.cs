using PaymentGateway.Domain.Repository.Query.Base;

namespace PaymentGateway.Persistance.Repository.Query.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        public QueryRepository() : base()
        {
        }
    }
}
