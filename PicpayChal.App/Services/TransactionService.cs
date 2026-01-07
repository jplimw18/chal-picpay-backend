using System.Diagnostics;
using MassTransit;
using PicpayChal.App.DTO;
using PicpayChal.App.Entities;
using PicpayChal.App.Enums;
using PicpayChal.App.Exceptions;
using PicpayChal.App.Messaging.Contracts;
using PicpayChal.App.Repositories;
using PicpayChal.App.Repositories.Interfaces;
using PicpayChal.App.Services.External;

namespace PicpayChal.App.Services;

public sealed class TransactionService(
    ITransactionRepository transactionRepository,
    IWalletRepository walletRepository,
    IAuthorizationApi authorization,
    IUnitOfWork unitOfWork,
    IPublishEndpoint publishEndpoint
)
{
    private readonly ITransactionRepository _transactionRepository = transactionRepository;
    private readonly IWalletRepository _walletRepository = walletRepository;
    private readonly IAuthorizationApi _authorization = authorization;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint = publishEndpoint;


    public async Task Transfer(TransferRequest request)
    {
        AsValidTransaction(request);

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
    
        await Authorize();
        await _unitOfWork.CommitAsync();

        await PublishEvent(transaction.Id);
    }

    private async Task Authorize()
    {
        var authResponse = await _authorization.AuthorizeTransfer();
        if (!authResponse.IsSuccessful)
            throw new TransactionException("Não foi possível obter resposta da autorização para transferência");

        if (!authResponse.Content.IsAuthorized)
            throw new TransactionException("Transferência não autorizada");
    }

    private async Task PublishEvent(long transactionId) =>
        await _publishEndpoint.Publish(new NotifyTransfer(transactionId));

    private static void AsValidTransaction(TransferRequest request)
    {
        if (request.Value <= 0.0m)
            throw new TransactionException("Valor de transferência deve ser maior que zero");

        if (request.PayerId == request.PayeeId)
            throw new TransactionException("O pagador e o recebedor devem ser diferentes");
    }
}
