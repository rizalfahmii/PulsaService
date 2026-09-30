using PulsakuService.DTOs.Product;

namespace PulsakuService.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();

        Task<ProductResponse> GetByIdAsync(long id);

        Task<ProductResponse> CreateAsync(CreateProductRequest request);

        Task<ProductResponse> UpdateAsync(
            long id,
            UpdateProductRequest request
        );
    }
}