using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Requests.Targets.Commands;
using TaskMgr.Domain.Entities;

namespace TaskMgr.Tests.Application.Cqrs.Targets.Commands;

[TestFixture]
public class AddTargetCommandHandlerTests
{
    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _repositoryMock = new Mock<IRepository<TargetEntity>>();

        var serviceProvider = _services
            .AddScoped<IRepository<TargetEntity>>(_ => _repositoryMock.Object)
            .AddScoped<IRequestHandler<AddTargetCommand, TargetEntity>, AddTargetCommandHandler>()
            .AddMediatR(typeof(AddTargetCommandHandler))
            .BuildServiceProvider();

        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<TargetEntity>()))
            .ReturnsAsync((TargetEntity entity) => entity)
            .Verifiable();
    }

    private IMediator _mediator;
    private Mock<IRepository<TargetEntity>> _repositoryMock;
    private IServiceCollection _services;

    [Test]
    public async Task AddTargetCommandHandler_Success()
    {
        // Arrange
        var userId = new Guid("6A518083-A57F-494D-BD96-945D72B8FBC4");
        var expectedResult = new TargetEntity(userId, "Test title", "Test description");
        var command = new AddTargetCommand(userId, expectedResult.Title, expectedResult.Content);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        result.Should().BeEquivalentTo(expectedResult, options =>
            options.Excluding(r => r.CreatedAt));
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<TargetEntity>()), Times.Once);
    }
}