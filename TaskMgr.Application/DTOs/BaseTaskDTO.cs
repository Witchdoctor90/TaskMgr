using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.DTOs;

public class BaseTaskDTO
{
    public BaseTaskDTO(User user, string title, string content)
    {
        User = user;
        Title = title;
        Content = content;  
    }

    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}