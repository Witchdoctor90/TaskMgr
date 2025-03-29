using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Infrastructure.DB;

public class Repository<T> : IRepository<T> where T : BaseEntity
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
        var existingEntity = await _context.Set<T>().FindAsync(entity.Id);
        if (existingEntity == null)
            throw new TaskEntityNotFoundException($"Entity with ID {entity.Id} not found.");

        _context.Entry(existingEntity).CurrentValues.SetValues(entity);

        await _context.SaveChangesAsync();
        return existingEntity;
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