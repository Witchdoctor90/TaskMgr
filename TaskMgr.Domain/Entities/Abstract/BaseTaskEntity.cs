using TaskMgr.Domain.Enums;

namespace TaskMgr.Domain.Entities.Abstract;

public abstract class BaseTaskEntity : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime { get; set; }
    public Status Status { get; set; } = Status.Created;
}