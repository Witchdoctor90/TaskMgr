using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;
using TaskMgr.Infrastructure.Identity;

namespace TaskMgr.Infrastructure.DB;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<RoutineEntity> Routines { get; set; }
    public DbSet<TargetEntity> Targets { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}