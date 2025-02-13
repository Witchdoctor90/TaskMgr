using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class AddRoutineCommand : IRequest<RoutineEntity>
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public AddRoutineCommand(Guid userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }
}

public class AddRoutineCommandHandler : IRequestHandler<AddRoutineCommand, RoutineEntity>
{
    private readonly IRepository<RoutineEntity> _repository;

    public AddRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        this._repository = repository;
    }

    public async Task<RoutineEntity> Handle(AddRoutineCommand request, CancellationToken cancellationToken)
    {
        var routine = new RoutineEntity(request.UserId, request.Title, request.Content);
        await _repository.AddAsync(routine);
        await _repository.SaveChangesAsync(cancellationToken);
        return routine;
    }
}