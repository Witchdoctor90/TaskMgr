using System.Linq.Expressions;

namespace TaskMgr.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<TEntity?> GetByIdAsync(Guid id);
    public Task<IEnumerable<TEntity>> GetAllAsync(Guid userId);
    public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<bool> DeleteAsync(Guid id);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}