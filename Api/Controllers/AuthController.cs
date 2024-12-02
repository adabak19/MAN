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
            Console.Write("whoop whoop");
            string token = GenerateJwt(profile);

            return Ok(token);
        }
        catch (Exception e)
        {
            Console.Write("whoop whoop");
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
    // return new List<Claim>
    // {
    //     new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, config["Jwt:Subject"] ?? ""),
    //     new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //     new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(), ClaimValueTypes.DateTime),
    //     new Claim(ClaimTypes.Name, profile.ProfileName),
    //     new Claim(ClaimTypes.Role, profile.Role)
    // };
}


}
