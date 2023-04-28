using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;

    public UserController(IMediator mediator, IConfiguration configuration)
    {
        this._mediator = mediator;
        this._configuration = configuration;
    }

    [HttpGet]
    public Task<string> Get()
    {
        string token = CreateToken();
        return Task.FromResult(token);
    }

    private string CreateToken()
    {
        //emails, id, username

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, "test"),
            new Claim(ClaimTypes.Email, ""),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenKey").Value ?? string.Empty));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
             claims: claims,
             expires: DateTime.Now.AddDays(1),
             signingCredentials: creds);

        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

        return jwtToken;

    }
}
