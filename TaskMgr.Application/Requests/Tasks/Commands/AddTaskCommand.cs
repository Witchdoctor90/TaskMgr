using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Tasks.Commands;

public class AddTaskCommand : IRequest<TaskEntity>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }

    public AddTaskCommand(string title, string content, Guid userId)
    {
        Title = title;
        Content = content;
        UserId = userId;
    }
}

public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, TaskEntity>
{
    private readonly IRepository<TaskEntity> _repository;

    public AddTaskCommandHandler(IRepository<TaskEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TaskEntity> Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskEntity(request.UserId, request.Title, request.Content);
        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync(cancellationToken);
        return task;
    }
}