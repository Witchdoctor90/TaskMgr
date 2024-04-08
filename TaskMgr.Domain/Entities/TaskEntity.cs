using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class TaskEntity : BaseTaskEntity
{
    public List<Tag> Tags { get; set; } = new List<Tag>();
}