using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Categories.Commands.CreateCommand;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context;
    
    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var obj = new Category()
        {
            Id = request.Id,
            Title = request.Title
        };
        await _context.Categories.AddAsync(obj);
        await _context.SaveChangesAsync(cancellationToken);
        return obj.Id;
    }
}