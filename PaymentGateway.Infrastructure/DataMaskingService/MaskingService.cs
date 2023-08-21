using PaymentGateway.Application.Interfaces;

namespace PaymentGateway.Infrastructure.DataMaskingService
{
    /// <summary>
    /// Data Masking Service
    /// </summary>
    public class MaskingService : IMaskingService
    {
        /// <summary>
        /// This method Masks Card Number
        /// </summary>
        /// <param name="cardNumber">string</param>
        /// <returns>string</returns>
        public async Task<string> MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                return string.Empty;
            }

            int visibleDigits = 4;
            int maskedLength = cardNumber.Length - visibleDigits;

            string maskedPart = new string('*', maskedLength);
            string visiblePart = cardNumber.Substring(maskedLength);

            return await Task.FromResult(maskedPart + visiblePart);
        }

        /// <summary>
        /// This method Masks card holders name 
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>string</returns>
        public async Task<string> MaskCardHolderName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return string.Empty;
            }

            string[] nameParts = name.Split(' ');
            for (int i = 1; i < nameParts.Length; i++)
            {
                nameParts[i] = new string('*', nameParts[i].Length);
            }

            return await Task.FromResult(string.Join(" ", nameParts));
        }
    }
}
