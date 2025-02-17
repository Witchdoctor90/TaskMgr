using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Targets.Commands;

public class AddTargetCommand : IRequest<TargetEntity>
{
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public AddTargetCommand(Guid userId, string title, string content)
    {
        UserId = userId;
        Title = title;
        Content = content;
    }
}

public class AddTargetCommandHandler : IRequestHandler<AddTargetCommand, TargetEntity>
{
    private readonly IRepository<TargetEntity> _repository;

    public AddTargetCommandHandler(IRepository<TargetEntity> repository)
    {
        _repository = repository; 
    }

    public async Task<TargetEntity> Handle(AddTargetCommand request, CancellationToken cancellationToken)
    {
        var target = new TargetEntity(request.UserId, request.Title, request.Content);
        await _repository.AddAsync(target);
        await _repository.SaveChangesAsync(cancellationToken);
        return target;
    }
}