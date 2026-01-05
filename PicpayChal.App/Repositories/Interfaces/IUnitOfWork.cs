namespace PicpayChal.App.Repositories.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}
