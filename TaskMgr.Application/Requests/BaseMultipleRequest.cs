using System.Collections;
using MediatR;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Requests;

public class BaseMultipleRequest<TResult> : IRequest<IEnumerable<TResult>>
where TResult : BaseEntity
{
    public User User { get; set; }

    public BaseMultipleRequest(User user)
    {
        User = user;
    }
}

public abstract class BaseMultipleRequestHandler<TResult> : IRequestHandler<BaseMultipleRequest<TResult>, IEnumerable<TResult>>
    where TResult : BaseEntity
{
    public abstract Task<IEnumerable<TResult>> Handle(BaseMultipleRequest<TResult> request,
        CancellationToken cancellationToken);
}