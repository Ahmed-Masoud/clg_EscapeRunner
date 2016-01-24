using EscapeRunner.Animations;
using EscapeRunner.View;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class Monster : IDrawable
    {
        private static Random random = new Random();
        private MonsterAnimation monsterAnimation;
        private PathFinder myPath;
        private List<IndexPair> tempPath;
        private bool increasing;
        private int counter;
        private Collider monsterCollider;

        public Monster()
        {
            increasing = true;
            counter = 0;
            AnimationFactory factory = new AnimationFactory();
            IndexPair temp = new IndexPair(1, 1);
            myPath = new PathFinder(new RouteInformation(temp, new IndexPair(15, 15)));
            tempPath = myPath.FindPath();
            //MapLoader.MonsterStartLocation;
            //monsterAnimation.AnimationTileIndex = new IndexPair(1, 1);//temp.TileIndecies;
            monsterAnimation = (MonsterAnimation)factory.CreateAnimation(AnimationType.MonsterAnimation, tempPath[0]);
            monsterAnimation.AnimationPosition = temp.IndexesToCorrdinates();
            

           
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
            NextStepIndex();
            monsterAnimation.AnimationPosition = tempPath[counter].IndexesToCorrdinates();

            // Update the collider's location
            this.monsterAnimation.ColliderLocationIndexes = tempPath[counter];
        }

        /// <summary>
        /// Counts the series 0...n.n-1...0 repeat for the monster's Path
        /// </summary>
        private void NextStepIndex()
        {
            if (increasing)
            {
                if (counter++ == tempPath.Count - 1)
                {
                    increasing = false;
                    counter -= 2;
                }
            }
            else
            {
                if (--counter == -1)
                {
                    increasing = true;
                    counter += 2;
                }
            }
        }
    }
}