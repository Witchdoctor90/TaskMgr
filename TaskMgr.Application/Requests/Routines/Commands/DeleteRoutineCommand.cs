using System.Reflection.Metadata;
using MediatR;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Requests.Routines.Commands;

public class DeleteRoutineCommand : IRequest<bool>
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }

    public DeleteRoutineCommand(Guid id, Guid userId)
    {
        Id = id;
        UserID = userId;
    }
}

public class DeleteRoutineCommandHandler : IRequestHandler<DeleteRoutineCommand, bool>
{
    private readonly IRepository<RoutineEntity> _repository;

    public DeleteRoutineCommandHandler(IRepository<RoutineEntity> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteRoutineCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity is null) throw new TaskEntityNotFoundException(request.Id);
        if(entity?.UserId != request.UserID) throw new UnauthorizedAccessException();
        
        
        await _repository.DeleteAsync(request.Id);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }
}