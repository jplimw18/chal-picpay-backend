namespace PicpayChal.App;

public interface INotificationService
{
    Task Notify(long transactionId);
}
