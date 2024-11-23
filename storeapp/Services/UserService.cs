using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using BookStore.EFLib.Models;

namespace storeapp.Services
{
    public class UserService : IUserService
    {
        private BookStore.EFLib.Models.BookStoreContext _context;
        private readonly List<User> _users = new List<User>();
        public UserService(BookStore.EFLib.Models.BookStoreContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(string username, string password)
        {
            var passwordHash = HashPassword(password);

            var user = new User
            {

                PasswordHash = passwordHash,

                Role = "User"
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || user.PasswordHash != HashPassword(password))
            {
                return null;
            }
            return user;
        }
    
        private static string HashPassword(string password) 
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);

        }

        public async Task<User> RegisterAsunc(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var user = new User
            {
                Username = username,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            _users.Add(user);
            return await Task.FromResult(user);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            var exists = _users.Any(u => u.Username == username);
            return await Task.FromResult(exists);
        }

        public Task<User> AuthenticateAsync(object username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
