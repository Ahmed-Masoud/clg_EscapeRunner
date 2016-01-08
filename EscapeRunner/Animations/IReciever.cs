namespace EscapeRunner.Animations
{
    public interface IReciever
    {
        #region Public Properties

        AnimationType Type { set; }

        #endregion

        #region Public Methods

        Animation GetAnimationCommandResult();

        #endregion
    }
}