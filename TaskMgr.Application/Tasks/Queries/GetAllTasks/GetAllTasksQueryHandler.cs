using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskMgr.Application.Interfaces;
using TaskMgr.Application.Targets.Queries.GetAll;

namespace TaskMgr.Application.Tasks.Queries.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, TaskListVm>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;

    public GetAllTasksQueryHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TaskListVm> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasksQuery =
            await _context.Tasks.ProjectTo<TaskLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        return new TaskListVm() { Tasks = tasksQuery };
    }
}