namespace TaskMgr.Service;

public interface IBaseRepository<T>
{
    public Task<T> GetById(Guid guid);
    public Task<IEnumerable<T>> GetAll();
    public Task Create(T entity);
    public void Delete(T entity);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}