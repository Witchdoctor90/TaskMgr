using MediatR;

namespace TaskMgr.Application.Tags.Commands.CreateTag;

public class CreateTagCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}