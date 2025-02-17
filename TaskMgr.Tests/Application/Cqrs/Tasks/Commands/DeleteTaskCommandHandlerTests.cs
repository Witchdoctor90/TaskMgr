using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Tasks.Commands;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Tasks.Commands;

[TestFixture]
public class DeleteTaskCommandHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<TaskEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<TaskEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(DeleteTaskCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => { return MockDatabase<TaskEntity>.Tasks.FirstOrDefault(r => r.Id == id); })
            .Verifiable();
        _repositoryMock.Setup(r => r.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) =>
        {
            var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault(r => r.Id == id);
            if (entity is null) throw new TaskEntityNotFoundException(id);
            MockDatabase<TaskEntity>.Tasks.Remove(entity);
            return true;
        });
    }

    private IMediator _mediator;
    private Mock<IRepository<TaskEntity>> _repositoryMock;
    private IServiceCollection _services;


    [Test]
    public async Task DeleteTaskCommandHandler_ThrowsUnauthorizedWhenUserIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault();
        var command = new DeleteTaskCommand(entity.Id, Guid.Empty);
        // Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task DeleteTaskCommandHandler_ThrowsExceptionWhenTaskIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault();
        var command = new DeleteTaskCommand(Guid.Empty, entity.UserId);
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task DeleteTaskCommandHandler_SuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.ObjectToDelete;
        var command = new DeleteTaskCommand(entity.Id, entity.UserId);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        result.Should().BeTrue();
        MockDatabase<TaskEntity>.Tasks.Should().NotContain(entity);
        _repositoryMock.Verify(r => r.DeleteAsync(entity.Id), Times.Once);
    }
}