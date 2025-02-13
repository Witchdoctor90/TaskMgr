namespace TaskMgr.Domain.Entities.Abstract;

public class BaseTaskEntity : BaseEntity
{
    public BaseTaskEntity(User user, string title, string content)
    {
        User = user;
        Title = title;
        Content = content;
    }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public TaskStatus Status { get; set; } = TaskStatus.Created;
    public User User { get; set; }
}