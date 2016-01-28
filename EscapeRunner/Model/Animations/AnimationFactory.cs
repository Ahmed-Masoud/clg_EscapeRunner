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
        private static Animation temp;

        public Animation CreateAnimation(AnimationType animationName, IndexPair startLocation)
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    temp = new PlayerAnimation();
                    break;

                case AnimationType.MonsterAnimation:
                    temp = new MonsterAnimation(); // new MonsterAnimation();
                    temp.AddCollider();
                    //((MonsterAnimation)temp).Collider = new Collider(((MonsterAnimation)temp));
                    break;
                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    temp = new BulletAnimation();
                    temp.AddCollider();
                    //((BulletAnimation)temp).Collider = new Collider(((BulletAnimation)temp));
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

        public static Animation CreateEmpyAnimation(AnimationType animationName)
        {
            IndexPair startLocation = new IndexPair(0, 0);
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    temp = new PlayerAnimation();
                    temp.AddCollider();
                    break;
                case AnimationType.MonsterAnimation:
                    temp = new MonsterAnimation(); // new MonsterAnimation();
                    temp.AddCollider();
                    break;

                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    temp = new BulletAnimation();
                    temp.AddCollider();
                    break;

                // The animation point will be set when its requested from the pool
                case AnimationType.ExplosionAnimation:
                    temp = new ExplosionAnimation();
                    temp.Collider = new Collider(temp, System.Drawing.Rectangle.Empty);
                    break;

                default:
                    return null;
            }
            return temp;
        }
    }
}