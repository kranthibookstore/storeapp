using BookStore.EFLib.Models;

namespace storeapp.Services
{
    public interface IUserService
    {
        Task <User> AuthenticateAsync (string username, string password);
        Task<User> AuthenticateAsync(object username, string password);
        Task <User> RegisterAsunc (string username, string password);

        Task<bool> UserExistsAsync(string username);
    }
}
