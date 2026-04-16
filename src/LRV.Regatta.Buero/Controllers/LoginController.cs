using LRV.Regatta.Buero.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LRV.Regatta.Buero.Controllers
{
    /// <summary>
    /// LoginController class that handles HTTP requests related to user authentication in the regatta management system. This controller includes endpoints for simulating a login check and generating JWT tokens for authenticated users. The controller uses JWT settings from the configuration to create and sign tokens, allowing clients to authenticate and access protected resources in the application.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private JwtSettings jwtSettings;

        /// <summary>
        /// Constructor for the LoginController class, initializing the JwtSettings dependency. This constructor sets up the necessary configuration for generating JWT tokens based on the application's settings, allowing the controller to handle authentication requests effectively.
        /// </summary>
        /// <param name="jwtOptions">The IOptions instance containing the JwtSettings configuration.</param>
        public LoginController(IOptions<JwtSettings> jwtOptions)
        {
            jwtSettings = jwtOptions.Value;
        }


        /// <summary>
        /// Processes the GET HTTP Verb, simulating a login check and returning a success message if the user is authenticated. This method serves as a placeholder for actual authentication logic, allowing clients to verify their login status before attempting to access protected resources in the application.
        /// </summary>
        /// <returns>An IActionResult indicating the result of the login check.</returns>
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

        /// <summary>
        /// Processes the POST HTTP Verb, handling user login requests and generating JWT tokens for authenticated users. This method validates the provided username and password, and if valid, creates a JWT token with the user's information and returns it to the client.
        /// </summary>
        /// <param name="loginRequest">The UserObject containing the username and password for authentication.</param>
        /// <returns>An IActionResult containing the JWT token if authentication is successful, or an Unauthorized result if authentication fails.</returns>
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
