using PicpayChal.App.Data;
using PicpayChal.App.Entities;

namespace PicpayChal.App.Repositories;

public sealed class TransactionRepository(AppDbContext context)
    : BaseRepository<Transaction>(context)
{

}
