using Invite.Commons.Notifications.Interfaces;

namespace Invite.Commons.Notifications;

public class NotificationContext : INotificationContext
{
    public int StatusCode { get; private set; }
    public string Title { get; private set; } = default!;
    public string Detail { get; private set; } = default!;
    public IDictionary<string, string[]> Notifications { get; } = new Dictionary<string, string[]>();
    public bool HasNotifications => StatusCode != 0 || Notifications.Count > 0;
    public bool IsEmptyNotificationList => Notifications.Count == 0;

    public void AddNotification(string field, string message)
    {
        Notifications.Add(field, [message]);
    }

    public void SetDetails(int statusCode, string title, string detail)
    {
        StatusCode = statusCode;
        Title = title;
        Detail = detail;
    }
}
