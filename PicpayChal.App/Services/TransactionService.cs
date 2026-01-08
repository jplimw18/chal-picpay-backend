using MassTransit;
using PicpayChal.App.DTO;
using PicpayChal.App.Entities;
using PicpayChal.App.Enums;
using PicpayChal.App.Exceptions;
using PicpayChal.App.Messaging.Contracts;
using PicpayChal.App.Repositories.Interfaces;
using PicpayChal.App.Services.Interfaces;

namespace PicpayChal.App.Services;

public sealed class TransactionService(
    ITransactionRepository _transactionRepository,
    IWalletRepository _walletRepository,
    IAuthorizationService _authorizationService,
    IUnitOfWork _unitOfWork
) : ITransactionService
{
    public async Task<long> Transfer(TransactionRequest request)
    {
        if (request.Value <= 0.0m)
            throw new TransactionException("Valor de transferência deve ser maior que zero");

        if (request.PayerId == request.PayeeId)
            throw new TransactionException("O pagador e o recebedor devem ser diferentes");

        var payer = await _walletRepository.GetById(request.PayerId);
        var payee = await _walletRepository.GetById(request.PayeeId);

        
        if (payer.Balance < request.Value)
            throw new TransactionException("Saldo insuficiente");

        if (payer.Type != (int)WalletType.Common)
            throw new TransactionException("Somente são permitidas transferências feitas por usuário do tipo comum");

        payer.Debit(request.Value);
        payee.Credit(request.Value);

        _walletRepository.Update(payer);
        _walletRepository.Update(payee);

        var transaction = _transactionRepository.Create(Transaction.Create(payer, payee, request.Value));
    
        await _authorizationService.Authorize();
        await _unitOfWork.CommitAsync();

        return transaction.Id;
    }
}
