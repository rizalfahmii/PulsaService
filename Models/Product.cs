namespace PulsakuService.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string Provider { get; set; } = string.Empty;

        public decimal Nominal { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TopupTransaction> Transactions { get; set; }
            = new List<TopupTransaction>();
    }
}