using Microsoft.EntityFrameworkCore;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;
using TaskMgr.Infrastructure;

namespace TaskMgr.Tests.Common;

public static class TargetsContextFactory
{
    public static Guid TargetIdForDelete = Guid.NewGuid();
    public static Guid TargetIdForUpdate = Guid.NewGuid();

    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var context = new AppDbContext(options);
        context.Database.EnsureCreated();
        context.Targets.AddRange(
            new Target()
            {
                Title = "Target1",
                Content = "Target1 Content",
                Id = Guid.Parse("A107FC4F-AECF-4B11-8BA3-440DF8680D08"),
                Status = Status.Created
            },
            new Target()
            {
                Title = "Target2",
                Content = "Target2 Content",
                Id = Guid.Parse("E36297B2-AB04-404E-B516-E599A29B65AA"),
                Status = Status.Created 
            },
            new Target()
            {
                Title = "Target for delete",
                Content = "Delete me",
                Id = TargetIdForDelete,
                Status = Status.Created
            },
            new Target()
            {
                Title = "Target for edit",
                Content = "Change this",
                Id = TargetIdForUpdate,
                Status = Status.Created
            }
            );
        context.SaveChanges();
        return context; 
    }

    public static void Destroy(AppDbContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
    
}