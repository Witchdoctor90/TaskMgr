using TaskMgr.Infrastructure;

namespace TaskMgr.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext _context;

    public TestCommandBase()
    {
        _context = TargetsContextFactory.Create();
    }

    public void Dispose()
    {
        TargetsContextFactory.Destroy(_context);
    }
}