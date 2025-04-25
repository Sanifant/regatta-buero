using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LRV.Regatta.Buero.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JwtSettings jwtSettings;

        public LoginController(IOptions<JwtSettings> jwtOptions)
        {
            jwtSettings = jwtOptions.Value;
        }


        [HttpGet]
        public IActionResult Get()
        {
            // Simulate a login check
            var isAuthenticated = true; // Replace with actual authentication logic
            if (isAuthenticated)
            {
                return Ok(new { message = "Login successful" });
            }
            else
            {
                return Unauthorized(new { message = "Login failed" });
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserObject loginRequest)
        {
            if (loginRequest.UserName == "testuser" && loginRequest.Password == "password")
            {
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new[]
                    {
                        new System.Security.Claims.Claim("sub", loginRequest.UserName),
                        new System.Security.Claims.Claim("role", "User")
                    }),

                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings.ExpiresInMinutes)),
                    Issuer = jwtSettings.Issuer,
                    Audience = jwtSettings.Audience,
                    SigningCredentials = creds
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwt = tokenHandler.WriteToken(token);

                return Ok(new { token = jwt });
            }

            return Unauthorized();
        }
    }
}
