namespace EscapeRunner.Animations
{
    /// <summary>
    /// Interface for Command design pattern for factory
    /// </summary>
    public interface IReciever
    {
        AnimationType Type { set; }

        Animation GetAnimationCommandResult();
    }
}