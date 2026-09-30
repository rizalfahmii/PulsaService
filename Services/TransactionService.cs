using Microsoft.EntityFrameworkCore;
using PulsakuService.Data;
using PulsakuService.DTOs.Transaction;
using PulsakuService.Models;

namespace PulsakuService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionResponse> CreateAsync(
            long userId,
            CreateTransactionRequest request)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x =>
                    x.ProductId == request.ProductId &&
                    x.IsActive
                );

            if (product == null)
            {
                throw new Exception("Product not found or inactive");
            }

            var transaction = new TopupTransaction
            {
                UserId = userId,
                ProductId = product.ProductId,
                PhoneNumber = request.PhoneNumber,

                // harga disalin dari product
                Price = product.Price,

                Status = "PENDING_PAYMENT",
                PaymentStatus = "PENDING",
                BillerStatus = "PENDING",

                CreatedAt = DateTime.UtcNow
            };

            _context.Transactions.Add(transaction);

            await _context.SaveChangesAsync();

            return new TransactionResponse
            {
                TransactionId = transaction.TransactionId,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                PhoneNumber = transaction.PhoneNumber,
                Price = transaction.Price,
                Status = transaction.Status,
                PaymentStatus = transaction.PaymentStatus,
                BillerStatus = transaction.BillerStatus,
                CreatedAt = transaction.CreatedAt
            };
        }

        public async Task<List<TransactionResponse>>
            GetMyTransactionsAsync(long userId)
        {
            return await _context.Transactions
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new TransactionResponse
                {
                    TransactionId = x.TransactionId,
                    ProductId = x.ProductId,
                    ProductName = x.Product.ProductName,
                    PhoneNumber = x.PhoneNumber,
                    Price = x.Price,
                    Status = x.Status,
                    PaymentStatus = x.PaymentStatus,
                    BillerStatus = x.BillerStatus,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<TransactionResponse> GetByIdAsync(
            long userId,
            long transactionId)
        {
            var transaction = await _context.Transactions
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x =>
                    x.TransactionId == transactionId &&
                    x.UserId == userId
                );

            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }

            return new TransactionResponse
            {
                TransactionId = transaction.TransactionId,
                ProductId = transaction.ProductId,
                ProductName = transaction.Product.ProductName,
                PhoneNumber = transaction.PhoneNumber,
                Price = transaction.Price,
                Status = transaction.Status,
                PaymentStatus = transaction.PaymentStatus,
                BillerStatus = transaction.BillerStatus,
                CreatedAt = transaction.CreatedAt
            };
        }
    }
}