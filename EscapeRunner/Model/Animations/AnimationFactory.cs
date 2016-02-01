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
        private static AnimationObject factoryTemp;

        public AnimationObject CreateAnimation(AnimationType animationName, IndexPair startLocation)
        {
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    factoryTemp = new PlayerAnimation();
                    break;

                case AnimationType.MonsterAnimation:
                    factoryTemp = new MonsterAnimation(); // new MonsterAnimation();
                    factoryTemp.AddCollider();
                    //((MonsterAnimation)temp).Collider = new Collider(((MonsterAnimation)temp));
                    break;
                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    factoryTemp = new BulletAnimation();
                    factoryTemp.AddCollider();
                    //((BulletAnimation)temp).Collider = new Collider(((BulletAnimation)temp));
                    break;
                // The animation point will be set when its requested from the pool
                case AnimationType.ExplosionAnimation:
                    factoryTemp = new ExplosionAnimation();
                    break;

                default:
                    return null;
            }
            return factoryTemp;
        }

        public static AnimationObject CreateEmpyAnimation(AnimationType animationName)
        {
            IndexPair startLocation = new IndexPair(0, 0);
            switch (animationName)
            {
                case AnimationType.PlayerAnimation:
                    factoryTemp = new PlayerAnimation();
                    factoryTemp.AddCollider();
                    break;

                case AnimationType.MonsterAnimation:
                    factoryTemp = new MonsterAnimation(); // new MonsterAnimation();
                    factoryTemp.AddCollider();
                    break;

                // The animation point will be set when its requested from the pool
                case AnimationType.BulletAnimation:
                    factoryTemp = new BulletAnimation();
                    factoryTemp.AddCollider();
                    break;

                // The animation point will be set when its requested from the pool
                case AnimationType.ExplosionAnimation:
                    factoryTemp = new ExplosionAnimation();
                    factoryTemp.Collider = new Collider(factoryTemp, System.Drawing.Rectangle.Empty);
                    break;

                default:
                    return null;
            }
            return factoryTemp;
        }
    }
}