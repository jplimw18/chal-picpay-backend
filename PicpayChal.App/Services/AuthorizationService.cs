using System.Security.Claims;
using PicpayChal.App.Exceptions;
using PicpayChal.App.Services.External;
using PicpayChal.App.Services.Interfaces;

namespace PicpayChal.App.Services;

public class AuthorizationService(IAuthorizationProvider authorizationApi)
    : IAuthorizationService
{
    private readonly IAuthorizationProvider _authorization = authorizationApi;

    public async Task Authorize()
    {
          var authResponse = await _authorization.AuthorizeTransfer();
        if (!authResponse.IsSuccessful)
            throw new TransactionException("Não foi possível obter resposta da autorização para transferência");

        if (!authResponse.Content.IsAuthorized)
            throw new TransactionException("Transferência não autorizada");
    }
}
