using PulsakuService.DTOs.Transaction;

namespace PulsakuService.Services
{
    public interface ITransactionService
    {
        Task<TransactionResponse> CreateAsync(
            long userId,
            CreateTransactionRequest request
        );

        Task<List<TransactionResponse>> GetMyTransactionsAsync(long userId);

        Task<TransactionResponse> GetByIdAsync(
            long userId,
            long transactionId
        );
    }
}