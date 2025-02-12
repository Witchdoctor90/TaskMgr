namespace TaskMgr.Domain.Entities.Abstract;

public class BaseTaskEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public TaskStatus Status { get; set; } = TaskStatus.Created;
    public bool isDeleted { get; set; } = false;
}