namespace PaymentGateway.Application.Responses
{
    public class ProcessPaymentResponse
    {
        public ProcessPaymentResponse()
        {
            this.PaymentSuccessResponse = new PaymentSuccessResponse();
            this.PaymentFailureResponse = new PaymentFailureResponse();
        }

        public bool IsSuccess { get; set; }

        public PaymentSuccessResponse PaymentSuccessResponse { get; set; }

        public PaymentFailureResponse PaymentFailureResponse { get; set; }
    }

    public class PaymentSuccessResponse
    {
        public Guid PaymentId { get; set; }

        public string SuccessMessage { get; set; }
    }

    public class PaymentFailureResponse
    {
        public IEnumerable<string> ValidationSummary { get; set; }

        public string ErrorMessage { get; set; }
    }
}
