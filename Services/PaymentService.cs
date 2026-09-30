using Microsoft.EntityFrameworkCore;
using PulsakuService.Data;
using PulsakuService.DTOs.Payment;
using PulsakuService.Models;

namespace PulsakuService.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly AppDbContext _context;
        private readonly IBillerService _billerService;

        public PaymentService(AppDbContext context, IBillerService billerService)
        {
            _context = context;
            _billerService = billerService;
        }

        public async Task<PaymentResponse> PayAsync(
            long userId,
            long transactionId,
            PayTransactionRequest request)
        {
            var transaction = await _context.Transactions
                .Include(x => x.Payment)
                .FirstOrDefaultAsync(x =>
                    x.TransactionId == transactionId &&
                    x.UserId == userId
                );

            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }

            if (transaction.Status != "PENDING_PAYMENT")
            {
                throw new Exception("Transaction cannot be paid");
            }

            if (transaction.Payment != null)
            {
                throw new Exception("Payment already exists");
            }

            var payment = new Payment
            {
                TransactionId = transaction.TransactionId,
                PaymentMethod = request.PaymentMethod,
                Amount = transaction.Price,
                PaymentStatus = "PAID",
                PaymentReference = $"DUMMY-{Guid.NewGuid()}",
                PaidAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };

            transaction.PaymentStatus = "PAID";
            transaction.Status = "PROCESSING";

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            await _billerService.SendToBillerAsync(transaction.TransactionId);

            return new PaymentResponse
            {
                PaymentId = payment.PaymentId,
                TransactionId = payment.TransactionId,
                PaymentMethod = payment.PaymentMethod,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus,
                PaymentReference = payment.PaymentReference,
                PaidAt = payment.PaidAt
            };
        }
    }
}