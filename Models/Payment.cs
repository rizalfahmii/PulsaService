namespace PulsakuService.Models
{
    public class Payment
    {
        public long PaymentId { get; set; }

        public long TransactionId { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public string PaymentStatus { get; set; } = "PENDING";

        public string? PaymentReference { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public TopupTransaction Transaction { get; set; } = null!;
    }
}
