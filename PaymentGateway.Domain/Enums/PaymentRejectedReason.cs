namespace PaymentGateway.Domain.Enums
{
    public enum PaymentRejectedReason
    {
        InsufficientBalance,

        IncorrectCardDetails,

        PaymentTimeOut,

        FraudPayment,

        DuplicatePayment
    }
}
