using MediatR;

namespace TaskMgr.Application.Tasks.Queries.GetTaskDetails;

public class GetTaskDetailsQuery : IRequest<TaskDetailsDto>
{
    public Guid Id { get; set; }
}