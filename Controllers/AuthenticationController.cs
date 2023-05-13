using dotnet_minimal_api.Models;
using dotnet_minimal_api.Services;
using Microsoft.AspNetCore.Mvc;
using JWT.Builder;
using JWT.Algorithms;
using System.Text.Json;
using System.Security.Cryptography.X509Certificates;
namespace DotNetSix;
using BCrypt.Net;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
  private readonly UsersService _usersService;
  private readonly X509Certificate2 certificate = X509Certificate2.CreateFromPemFile("certificates/cert.pem", "certificates/key.pem");

  public AuthenticationController(UsersService usersService) =>
      _usersService = usersService;

  [HttpGet("profile")]
  public async Task<ActionResult<String>> GetProfile()
  {
    try
    {
      if (HttpContext.Request.Headers["Authorization"] == "" || HttpContext.Request.Headers["Authorization"].ToString().Split("Bearer").Length <= 1)
        return Unauthorized();
      var token = HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1].Trim();
      var json = JwtBuilder.Create()
                           .WithAlgorithm(new RS256Algorithm(certificate))
                           .MustVerifySignature()
                           .Decode(token);
      User? user = JsonSerializer.Deserialize<User>(json);
      var checkDB = await _usersService.GetAsync(user?.Id);
      if (checkDB is null)
      {
        return Unauthorized();
      }
      return json;
    }
    catch (Exception e)
    {
      return e.ToString();
    }
  }

  [HttpPost("signup")]
  public async Task<IActionResult> SignUp(User newUser)
  {
    if (newUser.name is null || newUser.name == "")
      return BadRequest();
    if (newUser.role is null || newUser.role == "")
      return BadRequest();

    newUser.password = BCrypt.EnhancedHashPassword(newUser.password);
    await _usersService.CreateAsync(newUser);

    return CreatedAtAction(nameof(GetProfile), new { id = newUser.Id }, newUser);
  }

  [HttpPost("login")]
  public async Task<ActionResult<String>> Login(User req)
  {
    var user = await _usersService.GetAsyncByEmail(req.email);

    if (user is null || !BCrypt.EnhancedVerify(req.password, user.password))
    {
      return NotFound();
    }
    var token = JwtBuilder.Create()
                      .WithAlgorithm(new RS256Algorithm(certificate))
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim("Id", user.Id)
                      .AddClaim("email", user.email)
                      .AddClaim("name", user.name)
                      .Encode();
    return token;
  }
}