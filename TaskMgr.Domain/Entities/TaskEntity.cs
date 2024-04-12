using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class TaskEntity : BaseTaskEntity
{
    public Target? RelatedTarget { get; set; }
}