using Refit;

namespace PicpayChal.App.Services.External;

public interface IAuthorizationProvider
{
    [Get("/v2/authorize")]
    Task<ApiResponse<AuthData>> AuthorizeTransfer();
}
