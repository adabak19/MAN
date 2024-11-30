using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using System.Text;
using MAN.Shared.DTO;
using MAN.Shared.Models;
using MAN.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MAN.Shared.Interfaces;

namespace DNDProject.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration config, IAuthServiceAPI authService) : ControllerBase
{

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] ProfileLoginDto profileLoginDto)
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
    var key = config["Jwt:Key"];
    if (string.IsNullOrEmpty(key))
    {
        throw new Exception("JWT key is missing in configuration");
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, profile.ProfileName),
            new Claim(ClaimTypes.Role, profile.Role)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
        Issuer = config["Jwt:Issuer"],
        Audience = config["Jwt:Audience"]
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}


   private List<Claim> GenerateClaims(Profile profile)
{
    return new List<Claim>
    {
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime),
        new Claim(ClaimTypes.Name, profile.ProfileName),
        new Claim(ClaimTypes.Role, profile.Role)
    };
}


}
