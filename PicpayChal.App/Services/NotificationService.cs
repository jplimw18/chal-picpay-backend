using PicpayChal.App.Messaging.Publisher.Interfaces;

namespace PicpayChal.App.Services;

public class NotificationService(INotificationPublisher publisher)
    : INotificationService
{
    private readonly INotificationPublisher _publisher = publisher;

    public async Task Notify(long transactionId) =>
        await _publisher.NotifyTransaction(transactionId);
}
