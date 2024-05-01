using MediatR;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Routines.Queries.GetRoutineDetails;

public class GetRoutineDetailsQuery : IRequest<RoutineDetailsDto>
{
    public Guid Id { get; set; }
}