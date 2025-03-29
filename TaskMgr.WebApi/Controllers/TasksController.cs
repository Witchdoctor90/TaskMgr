using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskMgr.Application.Requests.Tasks.Commands;
using TaskMgr.Application.Requests.Tasks.Queries;
using TaskMgr.Domain.Entities;
using TaskMgr.WebApi.ViewModels;

namespace TaskMgr.WebApi.Controllers;

[Route("[controller]/[action]")]
public class TasksController : Controller
{
    private readonly ILogger<TasksController> _logger;
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator, ILogger<TasksController> logger)
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
        var query = new GetAllTasksQuery(userGuid);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        _logger.LogTrace("Incoming GetById request: \n User: {userGuid} \n Id:{id}", userGuid, id);
        var query = new GetTaskByIdQuery(userGuid, id);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] TaskEntityVM vm)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        _logger.LogTrace("Incoming Create request: \n User: {userGuid} \n Title:{title}", userGuid, vm.Title);
        var command = new AddTaskCommand(vm.Title, vm.Content, userGuid);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] TaskEntity entity)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"), out var userGuid)) return Unauthorized();
        _logger.LogTrace("Incoming Update request: \n User: {userGuid} \n Id:{id}", userGuid, entity.Id);
        var command = new UpdateTaskCommand(entity, userGuid);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete([FromBody] Guid taskId)
    {
        if (!Guid.TryParse(User.FindFirstValue("Id"),
                out var userGuid)) return Unauthorized();
        var command = new DeleteTaskCommand(taskId, userGuid);
        _logger.LogTrace("Incoming Delete request: \n User: {userGuid} \n Id:{id}", userGuid, taskId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }
}