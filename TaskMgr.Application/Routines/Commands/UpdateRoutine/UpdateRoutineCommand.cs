using MediatR;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Routines.Commands.UpdateRoutine;

public class UpdateRoutineCommand : IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime StartTime { get; set; }
    public Target? RelatedTarget { get; set; }
    public TimeSpanEnum TimeSpan { get; set; }
    public int TimeCount { get; set; }
    public List<Tag>? Tags { get; set; }
}