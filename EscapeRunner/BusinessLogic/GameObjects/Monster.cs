using EscapeRunner.Animations;
using System;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class Monster : IDrawable
    {
        private static Random random = new Random();
        private MonsterAnimation monsterAnimation;

        public Monster()
        {
            AnimationFactory factory = new AnimationFactory();
            monsterAnimation = (MonsterAnimation)factory.GetAnimationCommandResult(AnimationType.MonsterAnimation);
            // TODO set Monster start position at game start. TODO set Monster Direction at game start.
        }

        public static int DX { get; } = 5;
        public static int DY { get; } = 5;
        public Directions Direction { get; set; }
        public Point Position { get { return monsterAnimation.AnimationPosition; } set { monsterAnimation.AnimationPosition = value; } }

        public void Move(Directions direction)
        {
            Point newPosition = monsterAnimation.AnimationPosition;
            switch (direction)
            {
                case Directions.Up:
                    newPosition.Y -= DY;
                    break;

                case Directions.Down:
                    newPosition.Y += DY;
                    break;

                case Directions.Left:
                    newPosition.X -= DX;
                    break;

                case Directions.Right:
                    newPosition.X += DX;
                    break;
            }
            
            monsterAnimation.AnimationPosition = newPosition;
            monsterAnimation.LoadNextAnimationImage();
        }

        public void UpdateGraphics(Graphics g)
        {
            monsterAnimation.Draw(g, Direction);
        }
    }
}