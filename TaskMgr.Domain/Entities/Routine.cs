using TaskMgr.Domain.Entities.Abstract;
using TaskMgr.Domain.Enums;

namespace TaskMgr.Domain.Entities;
 
public class Routine : BaseTaskEntity
{
    public Target RelatedTarget { get; set; }
    public TimeSpanEnum TimeSpan { get; set; }
    public int TimeCount { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}