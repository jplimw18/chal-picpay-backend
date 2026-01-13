using Moq;
using PicpayChal.App;
using PicpayChal.App.DTO;
using PicpayChal.App.Entities;
using PicpayChal.App.Exceptions;
using PicpayChal.App.Repositories.Interfaces;
using PicpayChal.App.Services;
using PicpayChal.App.Services.Interfaces;

namespace PicpayChal.Test.Services;

public class TransactionServiceTest
{
    private readonly Mock<ITransactionRepository> _transactionRepoMock;
    private readonly Mock<IWalletRepository> _walletRepoMock;
    private readonly Mock<IAuthorizationService> _authorizationMock;
    private readonly Mock<IUnitOfWork> _uowMock;

    private readonly TransactionService _service;

    public TransactionServiceTest()
    {
        _transactionRepoMock = new Mock<ITransactionRepository>();
        _walletRepoMock = new Mock<IWalletRepository>();
        _authorizationMock = new Mock<IAuthorizationService>();
        _uowMock = new Mock<IUnitOfWork>();

        _service = new TransactionService(
            _transactionRepoMock.Object,
            _walletRepoMock.Object,
            _authorizationMock.Object,
            _uowMock.Object
        );
    }


    [Fact(DisplayName = "Deve realizar trasferência com sucesso")]
    public async Task Transferir_ComSaldo_TipoUsuarioComum()
    {
        var request = new TransactionRequest() { PayerId = 1, PayeeId = 2, Value = 50.0m };

        var payer = Wallet
            .CreateCommon("payer_test_success", "payer_test_success@test.test", "0000000000000", "test", 500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayerId))
            .ReturnsAsync(payer);

        var payee = Wallet
            .CreateShopkeeper("payee_test_success", "payee_test_success@test.test", "11111111111", "test", 500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayeeId))
            .ReturnsAsync(payee);

        _authorizationMock
            .Setup(auth => auth.Authorize())
            .Returns(Task.CompletedTask);

        await _service.Transfer(request);

        _walletRepoMock.Verify(walletRepo => walletRepo.Update(It.IsAny<Wallet>()), Times.Exactly(2));

        _uowMock.Verify(unitOfWork => unitOfWork.CommitAsync(), Times.Once());
    }


    [Fact(DisplayName = "Deve lançar exceção de saldo insuficiente ao realizar transferência")]
    public async Task Transferir_SemSaldo_TipoUsuarioComum()
    {
        var request = new TransactionRequest() { PayerId = 1, PayeeId = 2, Value = 50.0m };

        var payer = Wallet
            .CreateCommon("payer_test_fail", "payer_test_fail@test.test", "00000000000", "test", 45.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayerId))
            .ReturnsAsync(payer);

        var payee = Wallet
            .CreateCommon("payee_test_fail", "payee_test_fail@test.test", "00000000000", "test", 500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayeeId))
            .ReturnsAsync(payee);

        _authorizationMock
            .Setup(auth => auth.Authorize())
            .Returns(Task.CompletedTask);

        await Assert
            .ThrowsAsync<TransactionException>(async () => await _service.Transfer(request));
    }


    [Fact(DisplayName = "Deve lançar exceção de Tipo de usuário não tem permissão para realizar a operação")]
    public async Task Transferir_TipoUsuarioLojista()
    {
        var request = new TransactionRequest() { PayerId = 1, PayeeId = 2, Value = 500.0m };

        var payer = Wallet
            .CreateShopkeeper("payer_test_fail", "payer_test_fail@test.test", "00000000000", "test", 1500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayerId))
            .ReturnsAsync(payer);

        var payee = Wallet
            .CreateCommon("payee_test_fail", "payee_test_fail@test.test", "00000000000", "test", 2500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayeeId))
            .ReturnsAsync(payee);

        _authorizationMock
            .Setup(auth => auth.Authorize())
            .Returns(Task.CompletedTask);

        await Assert
            .ThrowsAsync<TransactionException>(async () => await _service.Transfer(request));

    }

    [Fact(DisplayName = "Deve lançar exceção do tipo Transação não autorizada/Falha na autorização")]
    public async Task Transferir_NaoAutorizado()
    {
        var request = new TransactionRequest() { PayerId = 1, PayeeId = 2, Value = 500.0m };

        var payer = Wallet  
            .CreateCommon("payer_unauthorized", "payer_unauthorized@test.test", "00000000000", "test", 5000.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayerId))
            .ReturnsAsync(payer);

        var payee = Wallet
            .CreateShopkeeper("payee_unauthorized", "payee_unauthorized@test.test", "00000000000", "test", 7500.0m);

        _walletRepoMock
            .Setup(walletRepo => walletRepo.GetById(request.PayeeId))
            .ReturnsAsync(payee);

        _authorizationMock
            .Setup(auth => auth.Authorize())
            .ThrowsAsync(new TransactionException("Transferência não autorizada"));

        await Assert
            .ThrowsAsync<TransactionException>(async () => await _service.Transfer(request));        
    }
}
