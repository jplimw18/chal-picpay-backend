using Refit;

namespace PicpayChal.App.Services.External;

public interface IAuthorizationApi
{
    [Get("/v2/authorize")]
    Task<ApiResponse<AuthData>> AuthorizeTransfer();
}
