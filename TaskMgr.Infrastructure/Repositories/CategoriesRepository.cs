using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Exceptions;
using TaskMgr.Service;
using Task = System.Threading.Tasks.Task;

namespace TaskMgr.Infrastructure.Repositories;

public class CategoriesRepository : RepositoryBase<Category>
{
    public CategoriesRepository(AppDbContext context) : base(context)
    {
    }
}