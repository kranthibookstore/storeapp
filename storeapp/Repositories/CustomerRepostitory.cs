using BookStore.EFLib.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using storeapp.Model;
using System.Reflection.Metadata.Ecma335;

namespace storeapp.Repositories
{
    public class CustomerRepostitory : ICustomerRepository
    {
        private readonly BookStoreContext _dbContext;

        public CustomerRepostitory(BookStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> AuthenticateCustomerAsync(string email, string password)
        {
            var customer = await _dbContext.Customers.SingleOrDefaultAsync(c => c.Email == email);
            if (customer != null && BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash))
                return customer;

            return null;
        }

        

        public async Task<bool> RegisterCustomerAsync(Customer customer)
        {
            if (await _dbContext.Customers.AnyAsync(c => c.Email == customer.Email))
                return false;

            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            _dbContext.Customers.Update(customer);
            await _dbContext.SaveChangesAsync();
        }

       
    }
}
