using Microsoft.EntityFrameworkCore;
using PulsakuService.Data;
using PulsakuService.DTOs.Product;
using PulsakuService.Models;

namespace PulsakuService.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            return await _context.Products
                .Where(x => x.IsActive)
                .Select(x => new ProductResponse
                {
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    Provider = x.Provider,
                    Nominal = x.Nominal,
                    Price = x.Price,
                    IsActive = x.IsActive
                })
                .ToListAsync();
        }

        public async Task<ProductResponse> GetByIdAsync(long id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Provider = product.Provider,
                Nominal = product.Nominal,
                Price = product.Price,
                IsActive = product.IsActive
            };
        }

        public async Task<ProductResponse> CreateAsync(
            CreateProductRequest request)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Provider = request.Provider,
                Nominal = request.Nominal,
                Price = request.Price,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Provider = product.Provider,
                Nominal = product.Nominal,
                Price = product.Price,
                IsActive = product.IsActive
            };
        }

        public async Task<ProductResponse> UpdateAsync(
            long id,
            UpdateProductRequest request)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.ProductId == id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.ProductName = request.ProductName;
            product.Provider = request.Provider;
            product.Nominal = request.Nominal;
            product.Price = request.Price;
            product.IsActive = request.IsActive;

            await _context.SaveChangesAsync();

            return new ProductResponse
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Provider = product.Provider,
                Nominal = product.Nominal,
                Price = product.Price,
                IsActive = product.IsActive
            };
        }
    }
}