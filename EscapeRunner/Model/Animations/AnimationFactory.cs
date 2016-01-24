using EscapeRunner.BusinessLogic;

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
        Animation temp;
        public Animation CreateAnimation(AnimationType animationName, IndexPair startLocation)
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    temp = new PlayerAnimation();
                    break;
                case AnimationType.MonsterAnimation:
                    temp = new MonsterAnimation(); // new MonsterAnimation();
                    ((MonsterAnimation)temp).Collider = new Collider(((MonsterAnimation)temp));
                    break;
                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    temp = new BulletAnimation();
                    ((BulletAnimation)temp).Collider = new Collider(((BulletAnimation)temp));
                    break;
                // The animation point will be set when its requested from the pool
                case AnimationType.ExplosionAnimation:
                    temp = new ExplosionAnimation();
                    break;
                default:
                    return null;
            }
            return temp;
        }
        public Animation CreateAnimation(AnimationType animationName)
        { return new PlayerAnimation(); }

    }
}