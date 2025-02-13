using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class UpdateRoutineCommand : IRequest<RoutineEntity>
{
    public RoutineEntity Routine { get; set; }

    public UpdateRoutineCommand(RoutineEntity routine)
    {
        Routine = routine;
    }
}

public class UpdateRoutineCommandHandler : IRequestHandler<UpdateRoutineCommand, RoutineEntity>
{
    private readonly IRepository<RoutineEntity> repository;

    public UpdateRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<RoutineEntity> Handle(UpdateRoutineCommand request, CancellationToken cancellationToken)
    {
        //todo auth
        
        return await repository.UpdateAsync(request.Routine);
    }
}