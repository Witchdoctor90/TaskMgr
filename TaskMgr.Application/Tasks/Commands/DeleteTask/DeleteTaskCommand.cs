using MediatR;

namespace TaskMgr.Application.Tasks.Commands;

public class DeleteTaskCommand : IRequest
{
    public Guid Id { get; set; }
}