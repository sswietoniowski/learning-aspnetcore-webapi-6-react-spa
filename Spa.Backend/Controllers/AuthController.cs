using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public class LoginRequestBody
    {
        [Required]
        [MaxLength(64)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        [MaxLength(128)]
        public string Password { get; set; } = string.Empty;
    }

    public class CityInfoUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;

        public CityInfoUser(
            int id, string userName, string firstName, string lastName, string city)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            City = city;                
        }
    }

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }        
        
    [HttpOptions("login")]
    public ActionResult<string> Login(LoginRequestBody request)
    {
        // Step 1: validate the username/password
        var user = ValidateUserCredentials(
            request.UserName, request.Password);
        
        if (user is null)
        {
            return Unauthorized();
        }

        // Step 2: create a token
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var signingCredentials = new SigningCredentials(
            securityKey, SecurityAlgorithms.HmacSha256);

        // The claims that 
        var claimsForToken = new List<Claim>();
        claimsForToken.Add(new Claim("sub", user.Id.ToString()));
        claimsForToken.Add(new Claim("given_name", user.FirstName));
        claimsForToken.Add(new Claim("family_name", user.LastName));
        claimsForToken.Add(new Claim("city", user.City));

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

    private CityInfoUser ValidateUserCredentials(string username, string password)
    {
        // we're assuming that the credentials are valid (just for the demo purposes)
        return new CityInfoUser(
            1, username ?? "", "John", "Doe", "Warsaw");
    }
}