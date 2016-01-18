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
        public ViewKey PressedKey { get; }
        public ViewEventArgs(ViewKey key)
        {
            this.PressedKey = key;
        }            
    }
}