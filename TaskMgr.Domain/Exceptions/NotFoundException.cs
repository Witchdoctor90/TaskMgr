namespace TaskMgr.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base("Item was not found")
    {
        
    }
}