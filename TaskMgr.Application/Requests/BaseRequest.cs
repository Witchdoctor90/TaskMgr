using MediatR;
using TaskMgr.Domain.Entities;
using TaskMgr.Domain.Entities.Abstract;

namespace TaskMgr.Application.Requests;

public class BaseRequest<TResult> : IRequest<TResult> where TResult : BaseEntity
{
    public User User { get; set; }

    public BaseRequest(User user)
    {
        User = user;
    }
}

public abstract class BaseRequestHandler<TResult> : IRequestHandler<BaseRequest<TResult>, TResult>
    where TResult : BaseEntity
{
    public abstract Task<TResult> Handle(BaseRequest<TResult> request, CancellationToken cancellationToken);
}