using System.Linq.Expressions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Tasks.Commands;
using TaskMgr.Application.Requests.Tasks.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Tasks.Queries;

public class GetAllTasksQueryHandlerTests
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
        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<TaskEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<TaskEntity, bool>> predicate)
                => MockDatabase<TaskEntity>.Tasks.FindAll(new Predicate<TaskEntity>(predicate.Compile())));
    }

    [Test]
    public async Task GetAllTasks_SingleSuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TaskEntity>.Tasks.FirstOrDefault();
        var query = new GetAllTasksQuery(entity.UserId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(r => r.Id == entity.Id);
    }

    [Test]
    public async Task GetAllTasks_MultipleSuccessWithValidParams()
    {
        // Arrange
        //user with 2 tasks
        var userID = Guid.Parse("59250DD9-C7BE-47C4-9616-5436265F42E0");
        var query = new GetAllTasksQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(r => r.Id == Guid.Parse("BC27A0C3-9178-4214-993E-8B474F0F80CA"))
            .And.Contain(r => r.Id == Guid.Parse("3FE3A70C-C927-4EAB-909F-5F2184EAA8CB"));
    }

    [Test]
    public async Task GetAllTasks_ReturnsEmptyCollectionWhenUserNotFound()
    {
        // Arrange
        var userID = Guid.Empty;
        var query = new GetAllTasksQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeNullOrEmpty();
    }
}