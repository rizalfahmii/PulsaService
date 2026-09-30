namespace PulsakuService.DTOs.Transaction
{
    public class TransactionResponse
    {
        public long TransactionId { get; set; }

        public long ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Status { get; set; } = string.Empty;

        public string PaymentStatus { get; set; } = string.Empty;

        public string BillerStatus { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}