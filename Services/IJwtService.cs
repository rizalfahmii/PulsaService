using PulsakuService.Models;

namespace PulsakuService.Services
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}