using PulsakuService.DTOs.Payment;

namespace PulsakuService.Services
{
    public interface IPaymentService
    {
        Task<PaymentResponse> PayAsync(
            long userId,
            long transactionId,
            PayTransactionRequest request
        );
    }
}