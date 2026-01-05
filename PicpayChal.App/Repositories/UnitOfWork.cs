using PicpayChal.App.Data;
using PicpayChal.App.Repositories.Interfaces;

namespace PicpayChal.App;

public sealed class UnitOfWork(AppDbContext context)
    : IUnitOfWork
{
    private readonly AppDbContext _context = context;

    public Task CommitAsync() =>
        _context.SaveChangesAsync();
}
