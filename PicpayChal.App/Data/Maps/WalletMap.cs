using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PicpayChal.App.Models;

namespace PicpayChal.App.Data.Maps
{
    public class WalletMap : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x => x.Id).HasName("walletId");

            builder.HasIndex(x => x.Cpf).IsUnique();
            builder.Property(x => x.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x =>x.Balance)
                .IsRequired()
                .HasPrecision(4);
        }
    }
}