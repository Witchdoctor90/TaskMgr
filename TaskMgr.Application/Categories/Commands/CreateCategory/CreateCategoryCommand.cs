using MediatR;

namespace TaskMgr.Application.Categories.Commands.CreateCommand;

public class CreateCategoryCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
}