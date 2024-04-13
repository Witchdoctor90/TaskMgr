using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Domain.Entities;

public class Target : BaseTaskEntity
{
    public List<Category> Categories { get; set; } = new List<Category>();
}