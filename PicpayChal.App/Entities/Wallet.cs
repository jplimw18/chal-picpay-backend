using PicpayChal.App.Enums;

namespace PicpayChal.App.Entities;

public record Wallet(string Name, string Email, string Cpf, string Password, int Type, decimal Balance)
{
    public long Id { get; private set; } = 0L;

    public IEnumerable<Transaction> Transactions { get; private set; } = [];

    private Wallet()
        : this(string.Empty, string.Empty, string.Empty, string.Empty, 0, 0.0m) { }
}

public static class WalletExtensions
{
    extension(Wallet)
    {
        public static Wallet CreateCommon(string name, string email, string cpf, string password, decimal value)
            => new(name, email, cpf, password, (int)WalletType.Common, value);

        public static Wallet CreateShopkeeper(string name, string email, string cpf, string password, decimal value)
            => new(name, email, cpf, password, (int)WalletType.Shopkeeper, value);
    }

    extension(Wallet wallet)
    {
        private Wallet Debit(decimal amount) =>
            wallet with { Balance = wallet.Balance - amount };

        private Wallet Credit(decimal amount) =>
            wallet with { Balance = wallet.Balance + amount };
    }
}