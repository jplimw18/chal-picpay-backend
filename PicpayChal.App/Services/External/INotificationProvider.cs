using PicpayChal.App.DTO;
using Refit;

namespace PicpayChal.App.Services.External;

public interface INotificationProvider
{
    [Post("v1/notify")]
    Task<NotificationData> Notify();
}
