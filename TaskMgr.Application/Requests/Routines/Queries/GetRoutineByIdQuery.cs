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
        this._repository = repository;
    }

    public async Task<RoutineEntity?> Handle(GetRoutineByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.RoutineId);
    }
}