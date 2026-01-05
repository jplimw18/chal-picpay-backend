using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicpayChal.App.Entities;

namespace PicpayChal.App.Data.Maps;

public sealed class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasOne(x => x.Payer)
        .WithMany(w => w.Transactions)
        .HasForeignKey(x => x.PayerId);

        builder.HasOne(x => x.Payee)
        .WithMany(w => w.Transactions)
        .HasForeignKey(x => x.PayeeId);

        builder.Property(x => x.Value).IsRequired();
    }
}
