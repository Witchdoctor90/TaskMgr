using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Infrastructure;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<Target> Targets { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
}