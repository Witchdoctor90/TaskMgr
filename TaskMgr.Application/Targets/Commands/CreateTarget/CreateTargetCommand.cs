using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Targets.Commands.CreateTarget;

public class CreateTargetCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<Tag>? Tags { get; set; }
}