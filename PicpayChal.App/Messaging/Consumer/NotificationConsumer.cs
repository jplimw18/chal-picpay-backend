using MassTransit;
using PicpayChal.App.Messaging.Contracts;
using PicpayChal.App.Services.External;

namespace PicpayChal.App.Messaging.Consumer;

public class NotificationConsumer(INotificationProvider notification)
    : IConsumer<NotifyTransaction>
{

    private readonly INotificationProvider _notification = notification;

    public async Task Consume(ConsumeContext<NotifyTransaction> context)
    {
        var message = context.Message;

        var notifyResponse = await _notification.Notify();
        if (!notifyResponse.Message)
            throw new NotificationException($"Falha ao enviar notificação para transferência: [{message.TransferId}]");
    }
}
