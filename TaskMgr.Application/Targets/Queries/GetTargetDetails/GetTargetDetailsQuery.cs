using MediatR;
using TaskMgr.Application.Targets.Queries.GetAll;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Targets.Queries.GetTargetDetails;

public class GetTargetDetailsQuery : IRequest<TargetDetailsDto>
{
    public Guid Id { get; set; }
}