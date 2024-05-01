using MediatR;

namespace TaskMgr.Application.Tags.Commands.UpdateTag;

public class UpdateTagCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
}