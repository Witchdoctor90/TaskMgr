using MediatR;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Target? RelatedTarget { get; set; }
    public Status Status { get; set; }
    public List<Tag> Tags { get; set; }
}