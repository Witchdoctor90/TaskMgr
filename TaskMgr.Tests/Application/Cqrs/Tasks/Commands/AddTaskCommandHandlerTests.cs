using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Tasks.Commands;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Tests.Application.Cqrs.Tasks.Commands;

public class AddTaskCommandHandlerTests
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
            .AddMediatR(typeof(AddTaskCommandHandler))
            .BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TaskEntity>()))
            .ReturnsAsync((TaskEntity entity) => entity);
    }

    [Test]
    public async Task AddTaskCommandHandler_Success()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var title = "Test Task";
        var content = "This is a test task content.";
        var command = new AddTaskCommand(title, content, userId);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(title);
        result.Content.Should().Be(content);
        result.UserId.Should().Be(userId);
        _repositoryMock.Verify(r => r.AddAsync(It.Is<TaskEntity>(t =>
            t.Title == title &&
            t.Content == content &&
            t.UserId == userId)), Times.Once);
    }
}