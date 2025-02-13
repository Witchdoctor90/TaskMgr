using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class TargetEntity : BaseTaskEntity
{
    public TargetEntity(Guid userId, string title, string content) : base(userId, title, content)
    {
    }

    public List<TaskEntity> Tasks { get; set; } = new();
    public List<RoutineEntity> Routines { get; set; } = new(); 
    public TimeSpan TimeSpended = TimeSpan.Zero;
}