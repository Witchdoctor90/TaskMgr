using MediatR;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Routines.Commands.CreateRoutine;

public class CreateRoutineCommand : IRequest<Guid>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DeletedAt { get; set; }
    public DateTime StartTime { get; set; }
    public Target? RelatedTarget { get; set; }
    public TimeSpanEnum TimeSpan { get; set; }
    public int TimeCount { get; set; }
    public List<Tag>? Tags { get; set; }
}