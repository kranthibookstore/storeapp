using BookStore.EFLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using storeapp.DTOs;
using storeapp.Services;
using System.Data;

namespace storeapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CustomerRegistrationDto registrationDto)
        {
            if (string.IsNullOrEmpty(registrationDto.Password) || string.IsNullOrEmpty(registrationDto.Email))
            {
                return BadRequest(new { message = "Email and Password are required." });
            }

            var result = await _customerService.RegisterCustomerAsync(registrationDto);

            if (!result)
                return Conflict(new { Message = "A customer with this email already exists." });

            return Ok(new { Message = "Register successful !." });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CustomerLoginDto loginDto)
        {

            var customer = await _customerService.LoginCustomerAsync(loginDto);

            if (customer == null)
                return Unauthorized(new { Message = "Invalid email or password." });

            return Ok(new
            {
                Message = "Login Successful",
                Customer = new
                {
                    customer.CustomerId,
                    customer.Name,
                    customer.Email,
                    customer.Phone,
                    customer.Address
                }
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest("Customer Id missmatch");
            }

            var existingCustomer = await _customerService.GetCustomerByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }

            existingCustomer.Name = customer.Name; ;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;

            await _customerService.UpdateCustomerAsync(existingCustomer);
            return NoContent();


        }


    }

}