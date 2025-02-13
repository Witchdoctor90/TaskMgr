using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class AddRoutineCommand : IRequest<RoutineEntity>
{
    public User User { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public AddRoutineCommand(User user, string title, string content)
    {
        this.User = user;
        Title = title;
        Content = content;
    }
}

public class AddRoutineCommandHandler : IRequestHandler<AddRoutineCommand, RoutineEntity>
{
    private readonly IRepository<RoutineEntity> repository;

    public AddRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        this.repository = repository;
    }

    public async Task<RoutineEntity> Handle(AddRoutineCommand request, CancellationToken cancellationToken)
    {
        var routine = new RoutineEntity(request.User, request.Title, request.Content);
        await repository.AddAsync(routine);
        await repository.SaveChangesAsync();
        return routine;
    }
}