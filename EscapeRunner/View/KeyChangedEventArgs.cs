namespace EscapeRunner
{
    public enum Notifing
    {
        Space,
        Right,
        Left,
        Down,
        Up
    }

    public class ViewNotificationEventArgs
    {
        public ViewNotificationEventArgs(Notifing notification)
        {
            this.Notification = notification;
        }

        public Notifing Notification { get; }
    }
}