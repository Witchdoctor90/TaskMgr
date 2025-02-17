using System.Linq.Expressions;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Targets.Commands;
using TaskMgr.Application.Requests.Targets.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.Tests.Application.Mocks;

namespace TaskMgr.Tests.Application.Cqrs.Targets.Queries;

public class GetAllTargetsQueryHandlerTests
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
        _repositoryMock.Setup(r => r.FindAsync(It.IsAny<Expression<Func<TargetEntity, bool>>>()))
            .ReturnsAsync((Expression<Func<TargetEntity, bool>> predicate)
                => MockDatabase<TargetEntity>.Tasks.FindAll(new Predicate<TargetEntity>(predicate.Compile())));
    }

    [Test]
    public async Task GetAllTargets_SingleSuccessWithValidParams()
    {
        // Arrange
        var entity = MockDatabase<TargetEntity>.Tasks.FirstOrDefault();
        var query = new GetAllTargetsQuery(entity.UserId);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(r => r.Id == entity.Id);
    }

    [Test]
    public async Task GetAllTargets_MultipleSuccessWithValidParams()
    {
        // Arrange
        //user with 2 tasks
        var userID = Guid.Parse("59250DD9-C7BE-47C4-9616-5436265F42E0");
        var query = new GetAllTargetsQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(r => r.Id == Guid.Parse("BC27A0C3-9178-4214-993E-8B474F0F80CA"))
            .And.Contain(r => r.Id == Guid.Parse("3FE3A70C-C927-4EAB-909F-5F2184EAA8CB"));
    }

    [Test]
    public async Task GetAllTargets_ReturnsEmptyCollectionWhenUserNotFound()
    {
        // Arrange
        var userID = Guid.Empty;
        var query = new GetAllTargetsQuery(userID);
        // Act
        var result = await _mediator.Send(query);
        // Assert
        result.Should().BeNullOrEmpty();
    }
}