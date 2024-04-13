using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    public Task<T> GetById(Guid guid);
    public Task<IEnumerable<T>> GetAll();
    public Task Create(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}