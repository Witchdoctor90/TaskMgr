using System.Linq.Expressions;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public TEntity GetById(object id);
    public IEnumerable<TEntity> GetAll();
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    public TEntity Add(TEntity entity);
    public TEntity Update(TEntity entity);
    public TEntity Delete(object id);
}