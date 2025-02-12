using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class TaskEntity : BaseTaskEntity
{
    public TaskEntity(User user, string title, string content) : base(user, title, content)
    {
    }

    public DateTime Deadline { get; set; } = DateTime.Today;
    public TimeSpan? RemindEvery { get; set; } = null;
    public RoutineEntity? RelatedRoutine { get; set; } = null;
    public TargetEntity? RelatedTarget { get; set; } = null;
    
}