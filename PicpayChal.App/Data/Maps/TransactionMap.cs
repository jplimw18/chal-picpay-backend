using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicpayChal.App.Models;

namespace PicpayChal.App.Data.Maps
{
    public sealed class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id)
                .HasName("transactionId");

            builder.Property(x => new { payerId = x.Payer.Id }).IsRequired();
            builder.HasOne(x => x.Payer)
                .WithMany()
                .HasForeignKey(x => new { payerId = x.Payer.Id });

            builder.Property(x => new { payeeId = x.Payee.Id }).IsRequired();
            builder.HasOne(x => x.Payee)
                .WithMany()
                .HasForeignKey(x => new { payeeId = x.Payee.Id });

            builder.Property(x => x.Value).IsRequired();

            builder.Property(x => x.CreatedAt)
                .ValueGeneratedOnAdd();
        }
    }
}