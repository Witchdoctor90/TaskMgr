using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Routines.Commands;
using TaskMgr.Application.Requests.Routines.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Routines.Queries;

public class GetRoutineByIdQueryHandlerTests
{
    private IMediator _mediator;
    private Mock<IRepository<RoutineEntity>> _repositoryMock;
    private IServiceCollection _services;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<RoutineEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<RoutineEntity>>(_ => _repositoryMock.Object)
            .AddMediatR(typeof(UpdateRoutineCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r
                => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Guid id) => { return MockDatabase<RoutineEntity>.Tasks.Find(r => r.Id == id); })
            .Verifiable();
    }

    [Test]
    public async Task GetRoutineByIdQueryHandler_Success()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault();
        var query = new GetRoutineByIdQuery(entity.UserId, entity.Id);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeEquivalentTo(entity);
        _repositoryMock.Verify(r => r.GetByIdAsync(entity.Id), Times.Once);
    }

    [Test]
    public async Task GetRoutineByIdQueryHandler_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault();
        var query = new GetRoutineByIdQuery(entity.UserId, Guid.Empty);
        // Act
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(query));
    }

    [Test]
    public async Task GetRoutineByIdQueryHandler_ThrowsUnauthorizedWhenUserIsInvalid()
    {
        // Arrange
        // Act
        // Assert
    }
}