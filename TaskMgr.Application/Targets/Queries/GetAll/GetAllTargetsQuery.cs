using MediatR;

namespace TaskMgr.Application.Targets.Queries.GetAll;

public class GetAllTargetsQuery : IRequest<TargetsListVm>
{
}