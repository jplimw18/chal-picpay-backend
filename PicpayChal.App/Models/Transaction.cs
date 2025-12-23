namespace PicpayChal.App.Models;


public record Transaction(
    Guid Id,
    Wallet Payer,
    Wallet Payee,
    decimal Value,
    DateTime? CreatedAt
);



public static class TransactionExtensions
{
    extension(Transaction)
    {
        public static Transaction Create(Wallet payer, Wallet payee, decimal value) =>
            new(Guid.NewGuid(), payer, payee, value, null);
    }
}