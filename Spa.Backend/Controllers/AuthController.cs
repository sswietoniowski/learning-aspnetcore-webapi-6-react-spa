using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository userRepository;

    public AuthController(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }        
        
    [HttpOptions("login")]
    public ActionResult<string> Login(LoginDto loginDto)
    {
        // Step 1: validate the username/password
        var user = userRepository.GetByUsernameAndPassword(loginDto.UserName, loginDto.Password);
            if (user == null)
                return Unauthorized();

        // Step 2: create a token
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        // The claims that 
        var claimsForToken = new List<Claim>()
        {
            new Claim("sub", user.Id.ToString()),
            new Claim("given_name", user.Name),
            new Claim("role", user.Role),
            new Claim("FavoriteColor", user.FavoriteColor)
        };

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claimsForToken,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        // You can test generated token contents here: https://jwt.io/

        return Accepted(new { Token = tokenToReturn });
    }

    [Authorize]
    public IActionResult GetUser()
    {
        return new JsonResult(User.Claims.Select(c => new { Type=c.Type, Value=c.Value }));
    }
}