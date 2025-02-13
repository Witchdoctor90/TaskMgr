using System.Linq.Expressions;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> GetByIdAsync(object id);
    public Task<IEnumerable<TEntity>> GetAllAsync();
    public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    public Task<TEntity> AddAsync(TEntity entity);
    public Task<TEntity> UpdateAsync(TEntity entity);
    public Task<TEntity> DeleteAsync(object id);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}