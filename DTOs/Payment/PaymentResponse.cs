namespace PulsakuService.DTOs.Payment
{
    public class PaymentResponse
    {
        public long PaymentId { get; set; }

        public long TransactionId { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; } = string.Empty;

        public string? PaymentReference { get; set; }

        public DateTime? PaidAt { get; set; }
    }
}