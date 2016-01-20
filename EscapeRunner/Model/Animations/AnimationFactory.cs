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
    public class AnimationFactory
    {

        public Animation GetAnimationCommandResult(AnimationType animationName)
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    return new PlayerAnimation();

                case AnimationType.MonsterAnimation:
                    return new MonsterAnimation(); // new MonsterAnimation();

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