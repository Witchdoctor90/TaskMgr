using System.Linq.Expressions;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity> GetById(object id);
    public Task<IEnumerable<TEntity>> GetAll();
    public Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate); 
    public Task<TEntity> Add(TEntity entity);
    public Task<TEntity> Update(TEntity entity);
    public Task<TEntity> Delete(object id);
}