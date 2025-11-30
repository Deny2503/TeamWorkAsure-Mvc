using Microsoft.AspNetCore.Identity;
using WebApplication16.Data.Models;

namespace WebApplication16.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordHasher<User> _hasher;

        public PasswordService(IPasswordHasher<User> hasher)
        {
            _hasher = hasher;
        }

        public string HashPassword(string password)
        {
            var tempUser = new User();
            return _hasher.HashPassword(tempUser, password);
        }

        public bool VerifyPassword(string hashedPassword, string password)
        {
            var tempUser = new User();
            var result = _hasher.VerifyHashedPassword(tempUser, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
