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

public class UpdateTaskCommandHandlerTests
{
    private IMediator _mediator;
    private Mock<IRepository<TaskEntity>> _repositoryMock;
    private IServiceCollection _services;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<TaskEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<TaskEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(UpdateTaskCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id)
                => MockDatabase<TaskEntity>.Tasks.FirstOrDefault(r => r.Id == id))
            .Verifiable();

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TaskEntity>()))
            .ReturnsAsync((TaskEntity entity) => entity);
    }

    [Test]
    public async Task UpdateTaskCommandHandler_SuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.objectToUpdate;
        var updatedEntity = new TaskEntity()
        {
            Id = entity.Id,
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = entity.UserId
        };
        var command = new UpdateTaskCommand(updatedEntity, entity.UserId);
        // Act
        var result = await _mediator.Send(command);
        // Assert
        result.Should().BeSameAs(updatedEntity);
        _repositoryMock.Verify(r
            => r.UpdateAsync(updatedEntity), Times.Once);
    }

    [Test]
    public async Task UpdateTaskCommandHandler_ThrowsUnauthorizedWhenUserIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.objectToUpdate;
        var updatedEntity = new TaskEntity()
        {
            Id = entity.Id,
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = Guid.Empty
        };
        var command = new UpdateTaskCommand(updatedEntity, entity.UserId);
        // Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task UpdateTaskCommandHandler_ThrowsExceptionWhenEntityNotExists()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.objectToUpdate;
        var updatedEntity = new TaskEntity()
        {
            Id = Guid.NewGuid(),
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = entity.UserId
        };
        var command = new UpdateTaskCommand(updatedEntity, entity.UserId);
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(command));
    }
}