using System.ComponentModel.DataAnnotations;

namespace PulsakuService.Models
{
    public class TopupTransaction
    {
        [Key]
        public long TransactionId { get; set; }

        public long UserId { get; set; }

        public long ProductId { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public string Status { get; set; } = "PENDING_PAYMENT";

        public string PaymentStatus { get; set; } = "PENDING";

        public string BillerStatus { get; set; } = "PENDING";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;

        public Product Product { get; set; } = null!;

        public Payment? Payment { get; set; }
    }
}