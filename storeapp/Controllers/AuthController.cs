using BookStore.EFLib.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace storeapp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        public IConfiguration _configuration;

        public AuthController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Post(string username = "test", string password = "test@123")
        {
            if (username != null && password != null)
            {
                var user = new User
                {
                    Id = 101,
                    Username = username,
                    PasswordHash = password,
                    DisplayName = "Kranthi Kumar",
                    Email = "kumar@mail.com",
                    UserId = "Q101",
                    FirstName = "Kranthi",
                    LastName = "Kumar"
                };

                if (user != null) //&& _configuration != null
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim("UserName", user.Username),
                        new Claim("Email", user.Email)
                    };

                    var jwtTocken = GenerateToken(user, claims);

                    return Ok(new { jwtTocken, user });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private string GenerateToken(User user, Claim[] claims)
        {
            // generate token that is valid for 30 minutes
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
