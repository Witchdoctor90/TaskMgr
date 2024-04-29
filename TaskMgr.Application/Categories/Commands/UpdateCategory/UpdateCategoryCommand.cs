using MediatR;

namespace TaskMgr.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
}