using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgr.WebApi.Interfaces;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUsersService _usersService;

    public AuthController(ILogger<AuthController> logger, IUsersService usersService)
    {
        _logger = logger;
        _usersService = usersService;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterVM vm)
    {
        try
        {
            var result = await _usersService.Register(vm);
            if (!result.Succeeded) return BadRequest("Failed to register user");
            var token = await _usersService.JWTLogin(vm.Username, vm.Password);
            return Ok(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest("Failed to register user");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginVM vm)
    {
        try
        {
            var result = await _usersService.JWTLogin(vm.Username, vm.Password);
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest("Failed to login");
        }
    }


    //remove this
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var username = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;

        return Ok(new { username = username, email = email });
    }
}