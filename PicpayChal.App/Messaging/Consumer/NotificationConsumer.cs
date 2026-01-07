using MassTransit;
using PicpayChal.App.Messaging.Contracts;
using PicpayChal.App.Services.External;

namespace PicpayChal.App.Messaging.Consumer;

public class NotificationConsumer(INotificationApi notification)
    : IConsumer<NotifyTransfer>
{

    private readonly INotificationApi _notification = notification;

    public async Task Consume(ConsumeContext<NotifyTransfer> context)
    {
        var message = context.Message;

        var notifyResponse = await _notification.Notify();
        if (!notifyResponse.Message)
            throw new NotificationException($"Falha ao enviar notificação para transferência: [{message.TransferId}]");
    }
}
