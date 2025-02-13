using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Queries;

public class GetRoutineByIdQuery : IRequest<RoutineEntity>
{
    public Guid UserId { get; set; }
    public Guid RoutineId { get; set; }

    public GetRoutineByIdQuery(Guid userId, Guid routineId)
    {
        UserId = userId;
        RoutineId = routineId;
    }
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
        if(result?.UserId != request.UserId) throw new UnauthorizedAccessException();
        return result;
    }
}