namespace PulsakuService.DTOs.Transaction
{
    public class CreateTransactionRequest
    {
        public long ProductId { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;
    }
}