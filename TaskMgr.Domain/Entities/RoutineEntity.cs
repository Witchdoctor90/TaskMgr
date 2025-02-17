using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class RoutineEntity : BaseTaskEntity
{
    public RoutineEntity()
    {
    }
    public RoutineEntity(Guid userId, string title, string content) : base(userId, title, content)
    {
    }

    public TimeSpan RepeatEvery { get; set; } = TimeSpan.Zero;
    public TargetEntity? RelatedTarget { get; set; } = null;
    public List<TaskEntity> RelatedTasks { get; set; } = new List<TaskEntity>();
}