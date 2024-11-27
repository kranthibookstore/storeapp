using BookStore.EFLib.Models;
using Org.BouncyCastle.Crypto.Generators;
using storeapp.DTOs;
using storeapp.Model;
using storeapp.Repositories;

namespace storeapp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository) 
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> RegisterCustomerAsync(CustomerRegistrationDto registrationDto)
        {
            var customer = new Customer
            {
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                Address = registrationDto.Address,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registrationDto.Password),
                RegisterAt = DateTime.UtcNow
            };

            return await _customerRepository.RegisterCustomerAsync(customer);
        }

        public async Task<Customer?> LoginCustomerAsync(CustomerLoginDto loginDto)
        {
            return await _customerRepository.AuthenticateCustomerAsync(loginDto.Email, loginDto.Password);
        }

    }
}
