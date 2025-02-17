using System.Linq.Expressions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Routines.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Routines.Queries;

public class FindRoutineQueryHandlerTests
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
            .AddMediatR(typeof(FindRoutineQueryHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<RoutineEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<RoutineEntity, bool>> predicate)
                => MockDatabase<RoutineEntity>.Tasks.FindAll(new Predicate<RoutineEntity>(predicate.Compile())));
    }

    [Test]
    public async Task FindRoutineQueryHandler_SingleSuccessWithValidParams()
    {
        // Arrange
        var userId = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault(r => r.Title == "Object3").UserId;
        var query = new FindRoutineQuery(r => r.Title == "Object3", userId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(r => r.Id == Guid.Parse("3BE556A3-44AD-4044-8612-0B023B3E3223"));
    }

    [Test]
    public async Task FindRoutineQueryHandler_MultipleSuccessWithValidParams()
    {
        // Arrange
        //User that has 2 tasks with the same title
        var userId = Guid.Parse("018970E8-B108-4D31-917B-316BFF1C5CAA");
        var query = new FindRoutineQuery(r => r.Title == "SameTitle", userId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(r => r.Id == Guid.Parse("58632684-8E21-4724-94A5-E8FBEF72D744"))
            .And.Contain(r => r.Id == Guid.Parse("190EC0AE-4486-45CF-A792-F3C11A94F29F"));
    }

    [Test]
    public async Task FindRoutineQueryHandler_SingleReturnFromMultipleTasksOwner()
    {
        // Arrange
        //User that has 2 tasks with different titles
        var userId = Guid.Parse("59250DD9-C7BE-47C4-9616-5436265F42E0");
        var query = new FindRoutineQuery(r => r.Title == "SameUser1", userId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(r => r.Id == Guid.Parse("BC27A0C3-9178-4214-993E-8B474F0F80CA"));
    }

    [Test]
    public async Task FindRoutineQueryHandler_ReturnsEmptyCollectionWhenNotFound()
    {
        // Arrange
        var userId = MockDatabase<RoutineEntity>.Tasks.FirstOrDefault(r => r.Title == "Object3").UserId;
        var query = new FindRoutineQuery(r => r.Title == "Not existing title", userId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeNullOrEmpty();
    }

    [Test]
    public async Task FindRoutineQueryHandler_ReturnsEmptyCollectionWhenUserNotFound()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new FindRoutineQuery(r => r.Title == "Object3", userId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeNullOrEmpty();
    }
}