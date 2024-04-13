using MediatR;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Targets.Commands.UpdateTarget;

public class UpdateTargetCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Tag> Tags { get; set; }
}