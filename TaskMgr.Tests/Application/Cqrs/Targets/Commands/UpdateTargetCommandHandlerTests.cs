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
public class UpdateTargetCommandHandlerTests
{
    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<TargetEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<TargetEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(UpdateTargetCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id)
                => MockDatabase<TargetEntity>.Tasks.FirstOrDefault(r => r.Id == id))
            .Verifiable();

        _repositoryMock.Setup(r => r.UpdateAsync(It.IsAny<TargetEntity>()))
            .ReturnsAsync((TargetEntity entity) => entity);
    }

    private IMediator _mediator;
    private Mock<IRepository<TargetEntity>> _repositoryMock;
    private IServiceCollection _services;

    [Test]
    public async Task UpdateTargetCommandHandler_SuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.objectToUpdate;
        var updatedEntity = new TargetEntity()
        {
            Id = entity.Id,
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = entity.UserId
        };
        var command = new UpdateTargetCommand(updatedEntity, entity.UserId);
        // Act
        var result = await _mediator.Send(command);
        // Assert
        result.Should().BeSameAs(updatedEntity);
        _repositoryMock.Verify(r
            => r.UpdateAsync(updatedEntity), Times.Once);
    }

    [Test]
    public async Task UpdateTargetCommandHandler_ThrowsUnauthorizedWhenUserIdIsInvalid()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.objectToUpdate;
        var updatedEntity = new TargetEntity()
        {
            Id = entity.Id,
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = Guid.Empty
        };
        var command = new UpdateTargetCommand(updatedEntity, entity.UserId);
        // Assert
        Assert.ThrowsAsync<UnauthorizedAccessException>(async () => await _mediator.Send(command));
    }

    [Test]
    public async Task UpdateTargetCommandHandler_ThrowsExceptionWhenEntityNotExists()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.objectToUpdate;
        var updatedEntity = new TargetEntity()
        {
            Id = Guid.NewGuid(),
            Title = "Updated Title",
            Content = "This is Updated Content",
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            UserId = entity.UserId
        };
        var command = new UpdateTargetCommand(updatedEntity, entity.UserId);
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(command));
    }
}