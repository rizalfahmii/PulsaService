namespace PulsakuService.DTOs.Product
{
    public class CreateProductRequest
    {
        public string ProductName { get; set; } = string.Empty;
        public string Provider { get; set; } = string.Empty;
        public decimal Nominal { get; set; }
        public decimal Price { get; set; }
    }
}