using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using MAN.Shared.DTO;
using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MAN.Shared.Interfaces;

namespace MAN.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration config, IAuthServiceAPI authService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<ActionResult> Login(ProfileLoginDto profileLoginDto)
    {
        try
        {
            Profile profile = await authService.ValidateUser(profileLoginDto.ProfileName, profileLoginDto.Password);
            string token = GenerateJwt(profile);

            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
private string GenerateJwt(Profile profile)
{
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(config["Jwt:Key"] ?? "");

    List<Claim> claims = GenerateClaims(profile);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = config["Jwt:Issuer"],
        Audience = config["Jwt:Audience"]
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}


   private List<Claim> GenerateClaims(Profile profile)
{
    var claims = new[]{
        new Claim(JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
        new Claim(ClaimTypes.Name, profile.ProfileName),
        new Claim(ClaimTypes.Role, profile.Role),
        new Claim("Username", profile.ProfileName),
        new Claim("Role", profile.Role),
    };
    return [.. claims];
    
    }
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync(Profile registerModel)
    {
        var result = await authService.RegisterUserAsync(registerModel);
        if (result)
        {
            return Ok();
        }
        return BadRequest("Registration failed");
    }
}



