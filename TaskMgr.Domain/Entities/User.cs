using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
}