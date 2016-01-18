using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    class Monster
    {
        private MonsterAnimation monsterAnimation;
        private static Random random = new Random();
        public Monster()
        {
            AnimationFactory factory = new AnimationFactory();
            monsterAnimation = (MonsterAnimation)factory.GetAnimationCommandResult();
            // TODO set Monster start position at game start.
            // TODO set Monster Direction at game start.
        }
        public Directions Direction { get; set; }
        public static int DX { get; } = 5;
        public static int DY { get; } = 5;
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
            monsterAnimation.LoadNextAnimationImage();
            monsterAnimation.AnimationPosition = newPosition;
        }
    }
}
