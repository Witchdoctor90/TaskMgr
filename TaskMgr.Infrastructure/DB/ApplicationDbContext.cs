using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Infrastructure.DB;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Task> Tasks { get; set; }
    public DbSet<RoutineEntity> Routines { get; set; }
    public DbSet<TargetEntity> Targets { get; set; }
}