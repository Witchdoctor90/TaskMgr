using TaskMgr.Domain.Entities;

namespace TaskMgr.Infrastructure.Repositories;

public class TasksRepository : RepositoryBase<TaskEntity>
{
    public TasksRepository(AppDbContext context) : base(context)
    {
    }
}