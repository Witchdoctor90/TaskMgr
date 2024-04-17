using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Targets.Commands.UpdateTarget;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Exceptions;
using TaskMgr.Tests.Common;
using Xunit;

namespace TaskMgr.Tests.Mediatr.Targets.Commands;

public class UpdateTargetHandlerTests : TestCommandBase
{
    [Fact]
    public async Task UpdateTargetHandler_Success()
    {
        //Arrange
        var handler = new UpdateTargetHandler(_context);
        var newTitle = "Updated Titel";
        var newContent = "Updated Content";
        var newTags = new List<Tag>()
        {
            new Tag()
            {
                Id = Guid.NewGuid(),
                Title = "Tag1"
            }
        };
        var entity = await _context.Targets.FirstOrDefaultAsync(t => t.Id == TargetsContextFactory.TargetIdForUpdate);
        
        //Act
        await handler.Handle(new UpdateTargetCommand()
        {
            Id = TargetsContextFactory.TargetIdForUpdate,
            Title = newTitle,
            Content = newContent,
            Tags = newTags
        }, CancellationToken.None);
        
        //Assert
        Assert.True(entity?.Title == newTitle && entity?.Content == newContent);
    }

    [Fact]
    public async Task UpdateCommandHandler_NotFound()
    {
        //Arrange
        var handler = new UpdateTargetHandler(_context);
        var newTitle = "Updated Title";
        var newContent = "Updated Content";
        var newTags = new List<Tag>();
        //Act
        //Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () => await handler.Handle(new UpdateTargetCommand()
        {
            Content = newContent,
            Tags = newTags,
            Title = newTitle,
            Id = Guid.NewGuid()
        }, CancellationToken.None));
    }
}