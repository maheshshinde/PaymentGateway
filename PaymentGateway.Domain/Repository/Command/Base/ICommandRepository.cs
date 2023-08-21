namespace PaymentGateway.Domain.Repository.Command.Base
{
    public interface ICommandRepository<T> where T : class
    {
       public Task<bool> InsertPaymentsAsync(T entity);
    }
}
