using MediatR;

namespace TaskMgr.Application.Targets.Commands.DeleteTarget;

public class DeleteTargetCommand : IRequest
{
    public Guid Id { get; set; }
}