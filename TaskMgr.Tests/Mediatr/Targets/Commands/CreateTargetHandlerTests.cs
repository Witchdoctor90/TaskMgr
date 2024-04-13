using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Targets.Commands.CreateTarget;
using TaskMgr.Infrastructure;
using TaskMgr.Tests.Common;
using Xunit;

namespace TaskMgr.Tests.Mediatr.Targets.Commands;

public class CreateTargetHandlerTests : TestCommandBase
{
    [Fact]
    public async Task CreateTargetCommandHandler_Success()
    {
        //Arrange 
        var handler = new CreateTargetCommandHandler(_context);
        var targetName = "Created Target";
        var targetContent = "I created this for test";
        
        //Act
        var targetId = await handler.Handle(new CreateTargetCommand()
        {
            Title = targetName,
            Content = targetContent,
            Id = Guid.NewGuid()
        }, CancellationToken.None);
        
        //Assert
        await _context.Targets.SingleOrDefaultAsync(t => t.Id == targetId);
    }
}