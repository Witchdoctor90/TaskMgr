using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Tasks.Commands;
using TaskMgr.Application.Requests.Tasks.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Tasks.Queries;

public class GetTaskByIdQueryHandlerTests
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
            .ReturnsAsync((Guid id) => { return MockDatabase<TaskEntity>.Tasks.Find(r => r.Id == id); })
            .Verifiable();
    }

    [Test]
    public async Task GetTaskByIdQueryHandler_Success()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault();
        var query = new GetTaskByIdQuery(entity.UserId, entity.Id);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeEquivalentTo(entity);
        _repositoryMock.Verify(r => r.GetByIdAsync(entity.Id), Times.Once);
    }

    [Test]
    public async Task GetTaskByIdQueryHandler_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault();
        var query = new GetTaskByIdQuery(entity.UserId, Guid.Empty);
        // Act
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(query));
    }
}