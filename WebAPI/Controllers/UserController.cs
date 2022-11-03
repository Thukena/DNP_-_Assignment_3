using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterface;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;
    private readonly IConfiguration _config;

    public UserController(IUserLogic userLogic, IConfiguration config)
    {
        _userLogic = userLogic;
        _config = config;
    }
    
    private List<Claim> GenerateClaims(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Id", user.Id.ToString())
        };
        return claims.ToList();
    }
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims, null, DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
 
    [HttpPost, Route("register")]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await _userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            User user = await _userLogic.GetAsync(dto);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    
    
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<User>>> GetAsync([FromQuery] string username, [FromQuery] string password)
    // {
    //     try
    //     {
    //         UserLoginDto dto = new(username, password);
    //         User user = await userLogic.GetAsync(dto);
    //         return Ok(user);
    //     }
    //     catch (Exception e)
    //     {
    //         Console.WriteLine(e);
    //         return StatusCode(500, e.Message);
    //     } 
    // }

    
}