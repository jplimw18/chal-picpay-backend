namespace PicpayChal.App.Messaging.Publisher.Interfaces;

public interface INotificationPublisher
{
        Task NotifyTransaction(long transactionId);
}
