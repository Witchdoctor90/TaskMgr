using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Exceptions;

public class TaskEntityNotFoundException : Exception
{
    public TaskEntityNotFoundException() : base("Task entity was not found!")
    {
    }

    public TaskEntityNotFoundException(string message) : base(message)
    {
    }

    public TaskEntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public TaskEntityNotFoundException(Guid entityId) : base($"Task entity with suggested id was not found: {entityId.ToString()}")
    {
    }
}