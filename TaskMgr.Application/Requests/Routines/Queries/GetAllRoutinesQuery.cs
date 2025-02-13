using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Queries;

public class GetAllRoutinesQuery : IRequest<IEnumerable<RoutineEntity>>
{
    public Guid UserId { get; set; }

    public GetAllRoutinesQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetAllRoutinesQueryHandler : IRequestHandler<GetAllRoutinesQuery, IEnumerable<RoutineEntity>>
{
    private readonly IRepository<RoutineEntity> _repository;

    public GetAllRoutinesQueryHandler(IRepository<RoutineEntity> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RoutineEntity>> Handle(GetAllRoutinesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.FindAsync(r => 
            r.UserId== request.UserId);
    }
}