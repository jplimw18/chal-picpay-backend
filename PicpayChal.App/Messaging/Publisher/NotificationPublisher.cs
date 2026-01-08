using MassTransit;
using PicpayChal.App.Messaging.Contracts;
using PicpayChal.App.Messaging.Publisher.Interfaces;

namespace PicpayChal.App.Messaging.Publisher;

public class NotificationPublisher(IPublishEndpoint publishEndpoint)
    : INotificationPublisher
{
    private readonly IPublishEndpoint _publishEnpoint = publishEndpoint;

    public async Task NotifyTransaction(long transactionId)
    {
        await _publishEnpoint.Publish(new NotifyTransaction(transactionId));
    }
}
