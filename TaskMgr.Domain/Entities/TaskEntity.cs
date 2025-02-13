using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class TaskEntity : BaseTaskEntity
{
    public TaskEntity(Guid userID, string title, string content) : base(userID, title, content)
    {
    }

    public DateTime Deadline { get; set; } = DateTime.Today;
    public TimeSpan? RemindEvery { get; set; } = null;
    public RoutineEntity? RelatedRoutine { get; set; } = null;
    public TargetEntity? RelatedTarget { get; set; } = null;
    
}