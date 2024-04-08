using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities.Abstract;
using TaskMgr.Domain.Exceptions;
using TaskMgr.Service;

namespace TaskMgr.Infrastructure.Repositories;

public abstract class RepositoryBase<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly AppDbContext _context;

    protected RepositoryBase(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetById(Guid guid)
    {
        var result = await _context.Set<T>().FirstOrDefaultAsync(item => item.Id == guid);
        if (result is null) throw new ItemNotFoundException(guid);
        return result;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        var result = await _context.Set<T>().ToListAsync();
        if (result is null) throw new NotFoundException();
        return result;
    }

    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }
    
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}