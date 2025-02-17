using MediatR;
using TaskMgr.Application.Exceptions;
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

public class UpdateTargetCommandHandler : IRequestHandler<UpdateTargetCommand, TargetEntity>
{
    private readonly IRepository<TargetEntity> _repository;

    public UpdateTargetCommandHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TargetEntity> Handle(UpdateTargetCommand request, CancellationToken cancellationToken)
    {
        if(request.Target.UserId != request.UserId) throw new UnauthorizedAccessException();
        if(await _repository.GetByIdAsync(request.Target.Id) is null) 
            throw new TaskEntityNotFoundException(request.Target.Id);
        
        return await _repository.UpdateAsync(request.Target);
    }
}