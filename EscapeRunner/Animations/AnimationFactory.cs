using EscapeRunner.Animations;

namespace EscapeRunner
{
    public enum AnimationType
    {
        PlayerAnimation,
        BulletAnimation,
        MonsterAnimation,
        SecondaryWeaponAnimation
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

                case AnimationType.SecondaryWeaponAnimation:
                    return null; // new SecondaryWeaponAnimation();
            }

            // Invalid case
            return null;
        }
    }
}