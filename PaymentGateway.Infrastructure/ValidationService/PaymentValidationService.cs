using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;

namespace PaymentGateway.Infrastructure.ValidationService
{
    public class PaymentValidationService : IPaymentValidationService
    {
        public async Task<IEnumerable<string>> ValidateData(ProcessPaymentsCommand payment)
        {
            if (payment == null)
            {
                return new List<string> {
                "Please provide all required fields."
                };
            }

            List<string> validationSummary = new();

            // *** Validate MerchantId
            if (payment.MerchantId == Guid.Empty)
            {
                validationSummary.Add("MerchantId is invalid.");
            }

            // *** Validate Amount
            if (payment.Amount <= 0)
            {
                validationSummary.Add("Amount must be greater than 0.");
            }

            // *** Validate Currency
            if (string.IsNullOrEmpty(payment.Currency) || !IsValidCurrencyCode(payment.Currency))
            {
                validationSummary.Add("Currency is invalid.");
            }

            validationSummary.AddRange(ValidateCardDetails(payment));

            return await Task.FromResult(validationSummary);
        }

        public List<string> ValidateCardDetails(ProcessPaymentsCommand cardDetails)
        {
            List<string> errors = new();

            // *** Validate CardNumber
            if (!IsValidCardNumber(cardDetails.CardNumber))
            {
                errors.Add("CardNumber is invalid.");
            }

            // *** Validate ExpiryMonth
            if (!IsValidExpiryMonth(cardDetails.ExpiryMonth))
            {
                errors.Add("ExpiryMonth is invalid.");
            }

            // *** Validate ExpiryYear
            if (!IsValidExpiryYear(cardDetails.ExpiryYear))
            {
                errors.Add("ExpiryYear is invalid.");
            }

            // *** Validate CVV
            if (!IsValidCVV(cardDetails.CVV))
            {
                errors.Add("CVV is invalid.");
            }

            // *** Validate CardHolderName
            if (string.IsNullOrEmpty(cardDetails.CardHolderName))
            {
                errors.Add("CardHolderName is required.");
            }

            return errors;
        }

        public bool IsValidCurrencyCode(string currencyCode)
        {
            // *** ToDo

            return true;
        }

        public bool IsValidCardNumber(string cardNumber)
        {
            if (cardNumber == null)
            {
                return false;
            }

            cardNumber = cardNumber.Replace(" ", "");

            // *** Check if the card number contains only digits
            if (!cardNumber.All(char.IsDigit))
            {
                return false;
            }

            // *** Ensure the card number has a valid length (typically 13 to 19 digits)
            if (cardNumber.Length < 13 || cardNumber.Length > 19)
            {
                return false;
            }

            return true;
        }

        public bool IsValidExpiryMonth(string expiryMonth)
        {
            if (expiryMonth == null)
            {
                return false;
            }

            // *** Expiry month should contain only digits
            if (!expiryMonth.All(char.IsDigit))
            {
                return false;
            }

            // *** Expiry month should be a valid month (between 1 and 12)
            int month = int.Parse(expiryMonth);
            return month >= 1 && month <= 12;
        }

        public bool IsValidExpiryYear(string expiryYear)
        {
            if (expiryYear == null)
            {
                return false;
            }

            //*** Expiry year should contain only digits
            if (!expiryYear.All(char.IsDigit))
            {
                return false;
            }

            // *** Expiry year should be a future year and not more than 10 years into the future
            int currentYear = DateTime.Now.Year;
            int year = int.Parse(expiryYear);
            int maxFutureYear = currentYear + 10;

            return year >= currentYear && year <= maxFutureYear;
        }

        public bool IsValidCVV(string cvv)
        {
            if (cvv == null)
            {
                return false;
            }

            //*** CVV should contain only digits
            if (!cvv.All(char.IsDigit))
            {
                return false;
            }

            //*** CVV length should be either 3 or 4 digits (depending on the card brand)
            if (cvv.Length < 3 || cvv.Length > 4)
            {
                return false;
            }

            return true;
        }
    }
}
