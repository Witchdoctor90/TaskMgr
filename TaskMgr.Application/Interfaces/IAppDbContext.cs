using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Application.Interfaces;

public interface IAppDbContext
{
    public DbSet<Target> Targets { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}