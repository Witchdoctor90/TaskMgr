using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class DeleteRoutineCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand, bool>
{
    private readonly IRepository<RoutineEntity> repository;

    public DeleteRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<bool> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(request.Id);
        await repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}