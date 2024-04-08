using TaskMgr.Domain.Entities;

namespace TaskMgr.Infrastructure.Repositories;

public class TargetsRepository : RepositoryBase<Target>
{
    public TargetsRepository(AppDbContext context) : base(context)
    {
    }
}