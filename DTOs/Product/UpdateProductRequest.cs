namespace PulsakuService.DTOs.Product
{
    public class UpdateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public decimal Nominal { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}