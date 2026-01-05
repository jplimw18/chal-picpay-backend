using Microsoft.EntityFrameworkCore;
using PicpayChal.App.Data;
using PicpayChal.App.Entities;
using PicpayChal.App.Repositories.Interfaces;

namespace PicpayChal.App.Repositories;

public sealed class WalletRepository(AppDbContext context)
        : BaseRepository<Wallet>(context)
{
    
}
