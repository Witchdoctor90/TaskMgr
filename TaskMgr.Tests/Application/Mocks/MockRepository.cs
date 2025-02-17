using System.Linq.Expressions;
using Moq;
using TaskMgr.Application.Exceptions;
using TaskMgr.Application.Interfaces;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Tests.Application.Mocks;

public static class MockDatabase<T>  where T : BaseTaskEntity, new()
{
    public static T ObjectToDelete = new T()
    {
        Id = new Guid("9C1FF382-6987-428A-B5B7-6E6F324FCBC1"),
        Title = "Object to delete",
        UserId = new Guid("819D69DB-6084-4D8B-9811-187E38309746"),
        Content = "This is object to delete it"
    };
    public static T objectToUpdate = new T() {
        Id = new Guid("B35706E8-9789-418A-B412-9445B11BE3AD"),
        Title = "Object to update",
        UserId = new Guid("C3CBFE90-C4CB-449F-B4A2-A60EB16FF9FB"),
        Content = "This is object to update it"
    };
    
    public static List<T> Tasks { get; set; } = new(new T[] { ObjectToDelete, objectToUpdate,new T() {
        Id = new Guid("3BE556A3-44AD-4044-8612-0B023B3E3223"),
        Title = "Object3",
        UserId = new Guid("693B7CB2-CE3B-4D0E-A9B7-63D3D83AA100"),
        Content = "This is object 3"
    },new T() {
        Id = new Guid("2EBD4D2E-1975-47FF-8CB7-D815C882259A"),
        Title = "Object4",
        UserId = new Guid("879054B5-ED43-4A86-8D8F-C4204F2363E9"),
        Content = "This is object 4"
    },new T() {
        Id = new Guid("577B0136-C1FB-4140-B113-797592A858EE"),
        Title = "Object5",
        UserId = new Guid("40B9DEEF-755A-47F3-B1D7-80F13EEC4595"),
        Content = "This is object 5"
    } });
    
    
    public static async Task<T?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(Tasks.Find(t => t.Id == id));
    }

    public static Task<IEnumerable<T>> GetAllAsync()
    {
        return Task.FromResult(Tasks.AsEnumerable());
    }

    public static async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Task.FromResult(Tasks.Where(predicate.Compile()).ToList());
    }

    public static Task<T> AddAsync(T entity)
    {
        Tasks.Add(entity);
        return Task.FromResult(entity);
    }

    public static Task<T> UpdateAsync(T entity)
    {
        var entityToUpdate = Tasks.Find(t => t.Id == entity.Id);
        if(entityToUpdate == null) throw new TaskEntityNotFoundException(entity.Id);
        entityToUpdate.Title = entity.Title;
        entityToUpdate.Content = entity.Content;
        entityToUpdate.Status = entity.Status;
        return Task.FromResult(entityToUpdate);
    }

    public static Task<bool> DeleteAsync(Guid id)
    {
        var entity = Tasks.Find(t => t.Id == id);
        if(entity == null) throw new TaskEntityNotFoundException(id);
        Tasks.Remove(entity);
        return Task.FromResult(true);
     }

    public static Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}