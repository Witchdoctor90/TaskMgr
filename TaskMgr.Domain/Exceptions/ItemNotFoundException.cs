namespace TaskMgr.Domain.Exceptions;

public class ItemNotFoundException : Exception
{
    public ItemNotFoundException(Guid id) : base($"The item with id {id} was not found!")
    {
        
    }
}