namespace EscapeRunner.Animations
{
    public enum AnimationType
    {
        PlayerAnimation,
        BulletAnimation,
        MonsterAnimation,
        ExplosionAnimation
    }

    /// <summary>
    /// Command design pattern
    /// </summary>
    public class AnimationFactory : IReciever
    {
        private AnimationType animationName;

        public AnimationType Type
        { set { animationName = value; } }

        public Animation GetAnimationCommandResult()
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    return new PlayerAnimation();

                case AnimationType.MonsterAnimation:
                    return null; // new MonsterAnimation();

                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    return new BulletAnimation();

                // The animation point will be set when its requested from the pool
                case AnimationType.ExplosionAnimation:
                    return new ExplosionAnimation();

                default:
                    return null;
            }
        }
    }
}