using Microsoft.EntityFrameworkCore;
using PicpayChal.App.Data.Maps;
using PicpayChal.App.Entities;

namespace PicpayChal.App.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WalletMap());
        modelBuilder.ApplyConfiguration(new TransactionMap());
        base.OnModelCreating(modelBuilder);
    }
}
