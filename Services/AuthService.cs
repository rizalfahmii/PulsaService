
using Microsoft.EntityFrameworkCore;
using PulsakuService.Data;
using PulsakuService.Models;

using PulsakuService.DTOs.Auth;
using PulsakuService.Services;

namespace PulsakuService.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthService(AppDbContext context, IJwtService jwtService)
        {
            this._context = context;
            this._jwtService = jwtService;
        }


        public async Task RegisterAsync (RegisterRequest request)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (existingUser != null)
            {
                throw new Exception("Email Already Registered");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = hashedPassword,
                Role = "USER",
                Status = "ACTIVE",
                CreatedAt = DateTime.UtcNow,

            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var users = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (users == null)
            {
                throw new Exception("invalid email or password!");
            }

            if(users.Status != "ACTIVE")
            {
                throw new Exception("User is Nonaktif!");
            }
            var password = BCrypt.Net.BCrypt.Verify(request.Password, users.Password);

            if(!password)
            {
                throw new Exception("Invalid email or password");
            }

            return new LoginResponse
            {
                Email = request.Email,
                Name = users.Name,
                Role = users.Role,
                Token = _jwtService.GenerateToken(users)
                  };
        }

    }
}
