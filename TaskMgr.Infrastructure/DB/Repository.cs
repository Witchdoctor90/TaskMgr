using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;

namespace TaskMgr.Infrastructure.DB;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _entities;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Guid userId)
    {
        return await _entities.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _entities.Where(predicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _entities.AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => _entities.Update(entity));
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException();
        _entities.Remove(entity);
        return true;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}