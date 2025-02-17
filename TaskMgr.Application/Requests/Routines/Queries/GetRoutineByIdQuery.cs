using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Queries;

public class GetRoutineByIdQuery : IRequest<RoutineEntity>
{
    public GetRoutineByIdQuery(Guid userId, Guid routineId)
    {
        UserId = userId;
        RoutineId = routineId;
    }

    public Guid UserId { get; set; }
    public Guid RoutineId { get; set; }
}

public class GetRoutineByIdQueryHandler : IRequestHandler<GetRoutineByIdQuery, RoutineEntity>
{
    private readonly IRepository<RoutineEntity> _repository;

    public GetRoutineByIdQueryHandler(IRepository<RoutineEntity> repository)
    {
        _repository = repository;
    }

    public async Task<RoutineEntity?> Handle(GetRoutineByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(request.RoutineId);
        if (result is null) throw new TaskEntityNotFoundException(request.RoutineId);
        if (result?.UserId != request.UserId) throw new UnauthorizedAccessException();
        return result;
    }
}