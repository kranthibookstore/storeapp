using BookStore.EFLib.Models;
using storeapp.Model;

namespace storeapp.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool>RegisterCustomerAsync (Customer customer);
        Task<Customer?> AuthenticateCustomerAsync( string email, string password );
    }
}
