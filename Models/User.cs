namespace PulsakuService.Models
{
    public class User
    {
        public long UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "USER";

        public string Status { get; set; } = "ACTIVE";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TopupTransaction> Transactions { get; set; }
            = new List<TopupTransaction>();
    }
}