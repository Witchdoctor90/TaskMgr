using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Routines.Commands;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Tests.Application.Cqrs.Routines.Commands;

[TestFixture]
public class AddRoutineCommandHandlerTests
{
    private IMediator _mediator;
    private Mock<IRepository<RoutineEntity>> _repositoryMock;
    private IServiceCollection _services;

    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<RoutineEntity>>();
        
        var serviceProvider = _services
            .AddScoped<IRepository<RoutineEntity>>(_ => _repositoryMock.Object)
            .AddScoped<IRequestHandler<AddRoutineCommand, RoutineEntity>, AddRoutineCommandHandler>()
            .AddMediatR(typeof(AddRoutineCommandHandler))
            .BuildServiceProvider();
        
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<RoutineEntity>()))
            .ReturnsAsync((RoutineEntity entity) => entity)
            .Verifiable();
    }

    [Test]
    public async Task AddRoutineCommandHandler_Success()
    {
        // Arrange
        var userId = new Guid("6A518083-A57F-494D-BD96-945D72B8FBC4");
        var expectedResult = new RoutineEntity(userId, "Test title", "Test description");
        var command = new AddRoutineCommand(userId, expectedResult.Title, expectedResult.Content);
        
        // Act
        var result = await _mediator.Send(command);
        
        // Assert
        result.Should().BeEquivalentTo(expectedResult, options => 
            options.Excluding(r => r.CreatedAt));
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<RoutineEntity>()), Times.Once);
    }
} 