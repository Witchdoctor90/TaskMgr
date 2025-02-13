namespace TaskMgr.Application.DTOs;

public class BaseTaskDTO
{
    public BaseTaskDTO(Guid userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;  
    }

    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}