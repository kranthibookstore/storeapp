using Microsoft.AspNetCore.Mvc;
using storeapp.DTOs;
using storeapp.Services;

namespace storeapp.Controllers
{
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
            if(string.IsNullOrEmpty(registrationDto.Password) || string.IsNullOrEmpty(registrationDto.Email))
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
    }
}
