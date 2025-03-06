using TaskMgr.Infrastructure.Identity;

namespace TaskMgr.WebApi.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}