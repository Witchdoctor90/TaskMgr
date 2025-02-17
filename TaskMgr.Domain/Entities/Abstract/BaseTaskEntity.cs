namespace TaskMgr.Domain.Entities.Abstract;

public class BaseTaskEntity : BaseEntity
{
    public BaseTaskEntity()
    {
    }
    protected BaseTaskEntity(Guid userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }

    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public TaskStatus Status { get; set; } = TaskStatus.Created;
    public Guid UserId { get; set; }
}