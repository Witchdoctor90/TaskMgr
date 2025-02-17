using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class UpdateRoutineCommand : IRequest<RoutineEntity>
{
    public RoutineEntity Routine { get; set; }
    public Guid UserId { get; set; }

    public UpdateRoutineCommand(RoutineEntity routine, Guid userId)
    {
        UserId = userId;
        Routine = routine;
    }
}

public class UpdateRoutineCommandHandler : IRequestHandler<UpdateRoutineCommand, RoutineEntity>
{
    private readonly IRepository<RoutineEntity> _repository;

    public UpdateRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        _repository = repository;
    }

    public async Task<RoutineEntity> Handle(UpdateRoutineCommand request, CancellationToken cancellationToken)
    {
        if(request.Routine.UserId != request.UserId) throw new UnauthorizedAccessException();
        if (await _repository.GetByIdAsync(request.Routine.Id) is null)
            throw new TaskEntityNotFoundException(request.Routine.Id);
        
        return await _repository.UpdateAsync(request.Routine);
    }
}