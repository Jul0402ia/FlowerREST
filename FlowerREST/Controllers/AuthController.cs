using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowerREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult Login(LoginRequest request)
        {
            // Simpelt login
            if (request.Username != "julia" ||
                request.Password != "1234")
            {
                return Unauthorized();
            }

            // Information som gemmes i token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username)
            };

            // Samme secret key som i Program.cs
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
"THIS_IS_A_LONG_SECRET_KEY_FOR_FLOWER_REST_JWT_123456789"));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            // Opret JWT-token
            var token = new JwtSecurityToken(
                issuer: "FlowerREST",
                audience: "FlowerRESTUsers",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            string tokenString =
                new JwtSecurityTokenHandler()
                    .WriteToken(token);

            return Ok(new
            {
                token = tokenString
            });
        }
    }

    public class LoginRequest
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }
} 