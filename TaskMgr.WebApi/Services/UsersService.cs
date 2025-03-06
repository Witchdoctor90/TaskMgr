using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TaskMgr.Infrastructure.Identity;
using TaskMgr.WebApi.Interfaces;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Services;

public class UsersService : IUsersService
{
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public UsersService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<IdentityResult> Register(RegisterVM vm)
    {
        var user = _mapper.Map<User>(vm);
        return await _userManager.CreateAsync(user, vm.Password);
    }

    public async Task<string> JWTLogin(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null) throw new KeyNotFoundException("Invalid username or password");
        return _tokenService.GenerateToken(user);
    }
}