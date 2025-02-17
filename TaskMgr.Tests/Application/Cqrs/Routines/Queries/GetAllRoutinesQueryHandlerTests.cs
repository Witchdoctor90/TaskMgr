using System.Linq.Expressions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Routines.Commands;
using TaskMgr.Application.Requests.Routines.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Routines.Queries;

public class GetAllRoutinesQueryHandlerTests
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
        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<RoutineEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<RoutineEntity, bool>> predicate)
                => MockDatabase<RoutineEntity>.Tasks.FindAll(new Predicate<RoutineEntity>(predicate.Compile())));
    }

    [Test]
    public async Task GetAllRoutines_SingleSuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault();
        var query = new GetAllRoutinesQuery(entity.UserId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(r => r.Id == entity.Id);
    }

    [Test]
    public async Task GetAllRoutines_MultipleSuccessWithValidParams()
    {
        // Arrange
        //user with 2 tasks
        var userID = Guid.Parse("59250DD9-C7BE-47C4-9616-5436265F42E0");
        var query = new GetAllRoutinesQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(r => r.Id == Guid.Parse("BC27A0C3-9178-4214-993E-8B474F0F80CA"))
            .And.Contain(r => r.Id == Guid.Parse("3FE3A70C-C927-4EAB-909F-5F2184EAA8CB"));
    }

    [Test]
    public async Task GetAllRoutines_ReturnsEmptyCollectionWhenUserNotFound()
    {
        // Arrange
        var userID = Guid.Empty;
        var query = new GetAllRoutinesQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeNullOrEmpty();
    }
}