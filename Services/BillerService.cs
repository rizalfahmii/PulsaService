using Microsoft.EntityFrameworkCore;
using PulsakuService.Data;

namespace PulsakuService.Services
{
    public class BillerService : IBillerService
    {
        private readonly AppDbContext _context;

        public BillerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task SendToBillerAsync(long transactionId)
        {
            var transaction = await _context.Transactions
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x =>
                    x.TransactionId == transactionId
                );

            if (transaction == null)
            {
                throw new Exception("Transaction not found");
            }

            if (transaction.PaymentStatus != "PAID")
            {
                throw new Exception("Transaction is not paid");
            }

            if (transaction.BillerStatus == "PROCESSING")
            {
                throw new Exception("Transaction already sent to biller");
            }

            if (transaction.BillerStatus == "SUCCESS")
            {
                throw new Exception("Transaction already completed");
            }

            transaction.BillerStatus = "PROCESSING";
            transaction.Status = "PROCESSING";

            await _context.SaveChangesAsync();

            // nanti kalau biller asli:
            // di sini kita call API biller pakai HttpClient

            // contoh data yang nanti dikirim:
            //
            // ProductCode = transaction.Product.ProductCode
            // PhoneNumber = transaction.PhoneNumber
            // Reference   = transaction.TransactionId
        }
    }
}