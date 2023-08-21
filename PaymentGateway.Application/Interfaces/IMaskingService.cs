namespace PaymentGateway.Application.Interfaces
{
    public interface IMaskingService
    {
        public Task<string> MaskCardNumber(string cardNumber);

        public Task<string> MaskCardHolderName(string name);
    }
}
