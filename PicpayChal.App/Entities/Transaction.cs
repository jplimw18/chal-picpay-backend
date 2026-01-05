namespace PicpayChal.App.Entities;

public record Transaction(Wallet Payer, Wallet Payee, decimal Value)
    : BaseEntity
{
    public long PayerId { get; private set; } = 0L;
    public long PayeeId { get; private set; } = 0L;

    private Transaction()
        : this(null!, null!, 0.0m) { }

}


public static class TransactionExtensions
{
    extension(Transaction)
    {
        public static Transaction Create(Wallet payer, Wallet payee, decimal value)
            => new(payer, payee, value);
    }
}