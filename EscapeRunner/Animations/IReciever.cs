namespace EscapeRunner.Animations
{
    public interface IReciever
    {
        AnimationType Type { set; }

        Animation GetAnimationCommandResult();
    }
}