using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Infrastructure.Repositories;

public class TagsRepository : RepositoryBase<Tag>
{
    public TagsRepository(AppDbContext context) : base(context)
    {
    }
}