using Microsoft.AspNetCore.Identity;

namespace TaskMgr.Infrastructure.Identity;

public class User : IdentityUser<Guid>
{
    public User() : base()
    {
    }
}