using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicpayChal.App.Entities;

namespace PicpayChal.App.Data.Maps;

public sealed class WalletMap : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired();

        builder.HasIndex(x => x.Cpf).IsUnique();
        builder.Property(x => x.Cpf).IsRequired();

        builder.Property(x => x.Balance).IsRequired();
    }
}
