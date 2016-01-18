namespace EscapeRunner
{
    public enum ViewKey
    {
        Space,
        Right,
        Left,
        Down,
        Up,
    }

    public class ViewEventArgs
    {
        public ViewEventArgs(ViewKey key)
        {
            this.PressedKey = key;
        }

        public ViewKey PressedKey { get; }
    }
}