using PaymentGateway.Application.Commands;
using PaymentGateway.Application.Interfaces;
using PaymentGateway.Application.Responses;
using PaymentGateway.Domain.Enums;

namespace PaymentGateway.Infrastructure.BankService
{
    public class MockBankService : IBankService
    {
        /// <summary>
        /// Acquring bank service mock, 
        /// keeping it simple for mock with few hard-code status based on the amount
        /// </summary>
        /// <param name="processPayments"></param>
        /// <returns>BankResponse</returns>
        public async Task<BankResponse> MakePayment(ProcessPaymentsCommand processPayments)
        {
            BankResponse bankResponse = new();

            // ***
            // *** Mock class for processing bank payments
            // *** Returning accepted and declined status based on hard coded amount value
            // ***
            if (processPayments.Amount > 1000)
            {
                bankResponse.PaymentStatus = PaymentStatus.Declined;
                bankResponse.RejectedReason = PaymentRejectedReason.FraudPayment;

                return await Task.FromResult(bankResponse);
            }
            else if (processPayments.Amount > 500 && processPayments.Amount < 1000)
            {
                bankResponse.PaymentStatus = PaymentStatus.Declined;
                bankResponse.RejectedReason = PaymentRejectedReason.InsufficientBalance;

                return await Task.FromResult(bankResponse);
            }

            bankResponse.PaymentStatus = PaymentStatus.Accepted;

            return await Task.FromResult(bankResponse);
        }
    }
}
