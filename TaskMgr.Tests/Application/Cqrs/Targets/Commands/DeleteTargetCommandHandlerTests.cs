using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Targets.Commands;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Targets.Commands;

[TestFixture]
public class DeleteTargetCommandHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<TargetEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<TargetEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(DeleteTargetCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => { return MockDatabase<TargetEntity>.Tasks.FirstOrDefault(r => r.Id == id); })
            .Verifiable();
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault(r => r.Id == id);
            if (entity is null) throw new TaskEntityNotFoundException(id);
            MockDatabase<TargetEntity>.Tasks.Remove(entity);
            return true;
        });
    }

    private IMediator _mediator;
    private Mock<IRepository<TargetEntity>> _repositoryMock;
    private IServiceCollection _services;


    [Test]
    public async Task DeleteTargetCommandHandler_ThrowsUnauthorizedWhenUserIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault();
        var command = new DeleteTargetCommand(entity.Id, Guid.Empty);
        // Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task DeleteTargetCommandHandler_ThrowsExceptionWhenTargetIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault();
        var command = new DeleteTargetCommand(Guid.Empty, entity.UserId);
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task DeleteTargetCommandHandler_SuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.ObjectToDelete;
        var command = new DeleteTargetCommand(entity.Id, entity.UserId);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        result.Should().BeTrue();
        MockDatabase<TargetEntity>.Tasks.Should().NotContain(entity);
        _repositoryMock.Verify(r => r.DeleteAsync(entity.Id), Times.Once);
    }
}