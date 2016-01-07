using EscapeRunner.Animations;

namespace EscapeRunner
{
    public enum AnimationType
    {
        PlayerAnimation,
        BulletAnimation,
        MonsterAnimation,
        ExplosionAnimation
    }

    public class AnimationFactory
    {
        public Animation CreateAnimation(AnimationType animationName)
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    return new PlayerAnimation();

                case AnimationType.BulletAnimation:
                    return new BulletAnimation();

                case AnimationType.MonsterAnimation:
                    return null; // new MonsterAnimation();

                case AnimationType.ExplosionAnimation:
                    return new ExplosionAnimation();
            }

            // Invalid case
            return null;
        }
    }
}