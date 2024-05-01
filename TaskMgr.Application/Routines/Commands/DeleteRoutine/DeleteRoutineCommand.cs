using MediatR;

namespace TaskMgr.Application.Routines.Commands.DeleteRoutine;

public class DeleteRoutineCommand : IRequest
{
    public Guid Id { get; set; }
}