using BookStore.EFLib.Models;
using storeapp.DTOs;
using storeapp.Model;

namespace storeapp.Services
{
    public interface ICustomerService
    {
        Task<bool> RegisterCustomerAsync(CustomerRegistrationDto registrationDto);
        Task<Customer?> LoginCustomerAsync(CustomerLoginDto loginDto);

        Task<Customer> GetCustomerByIdAsync(int id);
        Task UpdateCustomerAsync(Customer customer);
    }
}
