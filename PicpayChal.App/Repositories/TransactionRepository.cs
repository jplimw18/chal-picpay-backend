using PicpayChal.App.Data;
using PicpayChal.App.Entities;
using PicpayChal.App.Repositories.Interfaces;

namespace PicpayChal.App.Repositories;

public sealed class TransactionRepository(AppDbContext context)
    : BaseRepository<Transaction>(context), ITransactionRepository
{

}
