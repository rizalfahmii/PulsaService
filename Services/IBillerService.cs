namespace PulsakuService.Services
{
    public interface IBillerService
    {
        Task SendToBillerAsync(long transactionId);
    }
}