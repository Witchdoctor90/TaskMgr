using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Targets.Commands;
using TaskMgr.Application.Requests.Targets.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Targets.Queries;

public class GetTargetByIdQueryHandlerTests
{
    private IMediator _mediator;
    private Mock<IRepository<TargetEntity>> _repositoryMock;
    private IServiceCollection _services;

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
            .ReturnsAsync((Guid id) => { return MockDatabase<TargetEntity>.Tasks.Find(r => r.Id == id); })
            .Verifiable();
    }

    [Test]
    public async Task GetTargetByIdQueryHandler_Success()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault();
        var query = new GetTargetByIdQuery(entity.Id, entity.UserId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeEquivalentTo(entity);
        _repositoryMock.Verify(r => r.GetByIdAsync(entity.Id), Times.Once);
    }

    [Test]
    public async Task GetTargetByIdQueryHandler_ThrowsExceptionWhenNotFound()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault();
        var query = new GetTargetByIdQuery(Guid.Empty, entity.UserId);
        // Act
        // Assert
        Assert.ThrowsAsync<TaskEntityNotFoundException>(async () => await _mediator.Send(query));
    }

    [Test]
    public async Task GetTargetByIdQueryHandler_ThrowsUnauthorizedWhenUserIsInvalid()
    {
        // Arrange
        // Act
        // Assert
    }
}