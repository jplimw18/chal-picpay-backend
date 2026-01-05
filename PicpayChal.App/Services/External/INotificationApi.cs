using Refit;

namespace PicpayChal.App.Services.External;

public interface INotificationApi
{
    [Post("v1/notify")]
    Task Notify();
}
