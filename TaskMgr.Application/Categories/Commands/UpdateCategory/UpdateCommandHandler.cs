using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Exceptions;

namespace TaskMgr.Application.Categories.Commands.UpdateCategory;

public class UpdateCommandHandler : IRequestHandler<UpdateCategoryCommand, Guid>
{
    private readonly IAppDbContext _context;

    public UpdateCommandHandler(IAppDbContext context)
    {
        _context = context;
    }
    
    public async Task<Guid> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var obj = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id);
        if (obj is null) throw new ItemNotFoundException(request.Id);
        obj.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);
        return obj.Id;
    }
}