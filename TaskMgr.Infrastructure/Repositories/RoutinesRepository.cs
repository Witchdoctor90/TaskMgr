using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Exceptions;
using TaskMgr.Service;

namespace TaskMgr.Infrastructure.Repositories;

public class RoutinesRepository : RepositoryBase<Routine>
{
    public RoutinesRepository(AppDbContext context) : base(context)
    {
    }
}