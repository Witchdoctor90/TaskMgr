using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Routines.Commands;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Routines.Commands;

[TestFixture]
public class DeleteRoutineCommandHandlerTests
{
    private IMediator _mediator;
    private Mock<IRepository<RoutineEntity>> _repositoryMock;
    private IServiceCollection _services;
    
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<RoutineEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<RoutineEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(DeleteRoutineCommandHandler))
            .BuildServiceProvider();
        
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) =>
            {
                return MockDatabase<RoutineEntity>.Tasks.FirstOrDefault(r => r.Id == id);
            })
            .Verifiable();
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var entity = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault(r => r.Id == id);
            if (entity is null) throw new TaskEntityNotFoundException(id);
            MockDatabase<RoutineEntity>.Tasks.Remove(entity);
            return true;
        });
    }

    [Test]
    public async Task DeleteRoutineCommandHandler_SuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.ObjectToDelete;
        var command = new DeleteRoutineCommand(entity.Id, entity.UserId);
        
        // Act
        var result = await _mediator.Send(command);
        
        // Assert
        result.Should().BeTrue();
        MockDatabase<RoutineEntity>.Tasks.Should().NotContain(entity);
        _repositoryMock.Verify(r => r.DeleteAsync(entity.Id), Times.Once);
    }
    
    [Test]
    public async Task DeleteRoutineCommandHandler_ThrowsUnauthorizedWhenUserIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.ObjectToDelete;
        var command = new DeleteRoutineCommand(entity.Id, Guid.Empty);
        // Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _mediator.Send(command));
    }
    
    [Test]
    public async Task DeleteRoutineCommandHandler_ThrowsExceptionWhenRoutineIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.ObjectToDelete;
        var command = new DeleteRoutineCommand(Guid.Empty, entity.UserId);
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(command));
    }
}