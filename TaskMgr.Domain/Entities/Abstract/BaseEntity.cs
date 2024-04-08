namespace TaskMgr.Domain.Entities.Abstract;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Title { get; set; } = string.Empty;
}