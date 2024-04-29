using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Categories.Commands.DeleteCategory;

public class DeletecategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly IAppDbContext _context;

    public DeletecategoryCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (obj is null) throw new ItemNotFoundException(request.Id);
        _context.Categories.Remove(obj);
        await _context.SaveChangesAsync(cancellationToken);
    }
}