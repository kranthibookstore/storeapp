using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using storeapp.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BookStore.EFLib.Models;

namespace storeapp.Controllers
{

    [ApiController]
    [Route("api/auth")]
    //[Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService usersService, IConfiguration configuration)
        {
            _userService = usersService;
            _configuration = configuration;
        }

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        //    if (user == null) 
        //        return Unauthorized("Invalide username or password");

        //    var token = GenerateJwtToken(user);
        //    return Ok(new { Token =token  });
        //}

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        //{
        //    if (await _userService.UserExistsAsync(request.Username))
        //        return BadRequest("User already exists");

        //    var user = await _userService.RegisterAsunc(request.Username, request.Password);
            

        //    return Ok(new { Message = "User registred sucessfully" });

        //}


        private string GenerateJwtToken(User  user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role,user.Role),
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}



