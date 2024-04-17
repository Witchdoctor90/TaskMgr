using MediatR;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Tasks.Commands.CreateTask;

public class CreateTaskCommand : IRequest<Guid>
{
    public Guid Id = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content = string.Empty;
    public List<Tag>? Tags { get; set; }
    public Target? RelatedTarget { get; set; }
    public DateTime StartTime { get; set; }
}