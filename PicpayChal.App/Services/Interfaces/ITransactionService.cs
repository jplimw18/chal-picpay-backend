using PicpayChal.App.DTO;

namespace PicpayChal.App.Services.Interfaces;

public interface ITransactionService
{
    Task<long> Transfer(TransactionRequest request);
}
