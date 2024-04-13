using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Targets.Commands.DeleteTarget;
using TaskMgr.Domain.Exceptions;
using TaskMgr.Tests.Common;
using Xunit;

namespace TaskMgr.Tests.Mediatr.Targets.Commands;

public class DeleteTargetsHandlerTests : TestCommandBase
{
    [Fact]
    public async Task DeleteTargetHandler_Success()
    {
        //Arrange
        var handler = new DeleteTargetCommandHandler(_context);
        
        //Act
        await handler.Handle(new DeleteTargetCommand()
        {
            Id = TargetsContextFactory.TargetIdForDelete
        }, CancellationToken.None);
        
        //Assert
        Assert.Null(await _context.Targets.FirstOrDefaultAsync(t =>
            t.Id == TargetsContextFactory.TargetIdForDelete));
    }

    [Fact]
    public async Task DeleteTargetHandler_NotFound()
    {
        //Arrange
        var handler = new DeleteTargetCommandHandler(_context);
        
        //Act
        //Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(async () =>
            {
                await handler.Handle(new DeleteTargetCommand()
                    { Id = Guid.NewGuid() }, 
                    CancellationToken.None);
            });
    }
}