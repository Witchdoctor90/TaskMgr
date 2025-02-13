using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Commands;

public class UpdateTargetCommand : IRequest<TargetEntity>
{
    public TargetEntity Target { get; set; }
    public Guid UserId { get; set; }

    public UpdateTargetCommand(TargetEntity target, Guid userId)
    {
        Target = target;
        UserId = userId;
    }
}

public class UpdateCommandHandler : IRequestHandler<UpdateTargetCommand, TargetEntity>
{
    private readonly IRepository<TargetEntity> _repository;

    public UpdateCommandHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TargetEntity> Handle(UpdateTargetCommand request, CancellationToken cancellationToken)
    {
        return await _repository.UpdateAsync(request.Target);
    }
}