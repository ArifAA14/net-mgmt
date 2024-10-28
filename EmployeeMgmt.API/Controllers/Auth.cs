using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeMgmt.API.Controllers
{
  [ApiController]
  [Route("auth")]
  public class AuthController : ControllerBase
  {
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
      // For simplicity, no user validation. Assume login is successful.

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes("ThisIsASecureKeyThatIsLongEnough12345");
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, loginRequest.Username)
                }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = "your-app-issuer",
        Audience = "your-app-audience"
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      return Ok(new { Token = tokenString });
    }
  }

  public class LoginRequest
  {
    public string Username { get; set; }
  }
}
