namespace PicpayChal.App.Models;


public record Wallet(
    Guid Id,
    string Cpf,
    string Name,
    string Email,
    string Password,
    int Type,
    decimal Balance
);



public static class WalletExtensions
{
    extension(Wallet)
    {
        public static Wallet Create(string cpf, string name, string email, string password, int type, decimal balance) =>
            new(Guid.NewGuid(), cpf, name, email, password, type, balance);
    }


    extension(Wallet wallet)
    {
        private Wallet Debit(decimal value) => 
            wallet with { Balance = wallet.Balance - value };


        private Wallet Credit(decimal value) =>
            wallet with { Balance = wallet.Balance + value };
    }
}