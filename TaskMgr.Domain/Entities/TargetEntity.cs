namespace TaskMgr.Domain.Entities;

public class TargetEntity
{
    public List<TaskEntity> Tasks { get; set; } = new();
    public List<RoutineEntity> Routines { get; set; } = new(); 
    public TimeSpan TimeSpended = TimeSpan.Zero;
}