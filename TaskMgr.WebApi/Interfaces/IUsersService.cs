using Microsoft.AspNetCore.Identity;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Interfaces;

public interface IUsersService
{
    public Task<IdentityResult> Register(RegisterVM vm);
    public Task<string> JWTLogin(string username, string password);
}