using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgr.Application.Requests.Targets.Commands;
using TaskMgr.Application.Requests.Targets.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Controllers;

[Route("[controller]/[action]")]
public class TargetsController : Controller
{
    private readonly ILogger<TargetsController> _logger;
    private readonly IMediator _mediator;

    public TargetsController(IMediator mediator, ILogger<TargetsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        _logger.LogTrace("Incoming GetAll request: \n User: {userGuid}", userGuid);
        var query = new GetAllTargetsQuery(userGuid);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid))
            return Unauthorized();
        _logger.LogTrace("Incoming GetById request: \n User: {userGuid} \n Id:{id}", userGuid, id);
        var query = new GetTargetByIdQuery(userGuid, id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] TaskEntityVM vm)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        var command = new AddTargetCommand(userGuid, vm.Title, vm.Content);
        _logger.LogTrace("Incoming Create request: \n User: {userGuid} \n Title:{title}", userGuid, vm.Title);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] TargetEntity entity)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        var command = new UpdateTargetCommand(entity, userGuid);
        _logger.LogTrace("Incoming Update request: \n User: {userGuid} \n Id:{id}", userGuid, entity.Id);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromBody] Guid targetId)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"),
                out var userGuid)) return Unauthorized();
        var command = new DeleteTargetCommand(targetId, userGuid);
        _logger.LogTrace("Incoming Update request: \n User: {userGuid} \n Id:{id}", userGuid, targetId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}