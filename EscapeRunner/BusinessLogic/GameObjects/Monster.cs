using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class Monster : IDrawable
    {
        private static Random random = new Random();
        private MonsterAnimation monsterAnimation;
        private PathFinder myPath;
        private List<IndexPair> tempPath;
        private System.Timers.Timer makeStepTimer = new System.Timers.Timer();
        private bool increasing;
        private int counter;

        // TODO change to state
        public bool Alive { get; set; }

        public Monster(IndexPair startPoint, IndexPair endPoint)
        {
            increasing = true;
            counter = 0;
            AnimationFactory factory = new AnimationFactory();

            myPath = new PathFinder(new RouteInformation(startPoint, endPoint));
            tempPath = myPath.FindPath();
            makeStepTimer.Elapsed += Timer_Elapsed;
            makeStepTimer.Enabled = true;
            makeStepTimer.Interval = 250;
            Alive = true;

            //MapLoader.MonsterStartLocation;
            //monsterAnimation.AnimationTileIndex = new IndexPair(1, 1);//temp.TileIndecies;

            monsterAnimation = (MonsterAnimation)factory.CreateAnimation(AnimationType.MonsterAnimation, tempPath[0]);
            monsterAnimation.AnimationPosition = startPoint.IndexesToCorrdinates();

            // TODO set Monster start position at game start. TODO set Monster Direction at game start.
            monsterAnimation.Collider.Collided += Monster_Collided;
        }
        private void GraphicsSynchronizationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            monsterAnimation.LoadNextAnimationImage();
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            NextStepIndex();
        }

        private void Monster_Collided(CollisionEventArgs e)
        {
            // Game over
            if (e.CollidingObject.ToString().Equals("player"))
            {
                monsterAnimation.Collider.Active = false;
                Controller.GameOver(1);
                AudioController.PlayTerroristSound();
                AudioController.PlayExplosion();
            }

            if (e.CollidingObject.ToString().Equals("bullet"))
            {
                monsterAnimation.Collider.Active = false;
                Alive = false;
                Controller.Score += 20; // Add score
                AudioController.PlayMonsterDieSound();
            }
        }

        public Directions Direction { get; set; }
        public Point Position { get { return monsterAnimation.AnimationPosition; } set { monsterAnimation.AnimationPosition = value; } }

        public Point DrawLocation
        {
            get
            {
                return monsterAnimation.AnimationPosition;
            }
        }


        public void UpdateGraphics(Graphics g)
        {
            // TODO Check state
            monsterAnimation.Draw(g, Direction);

            // Update the collider's location
            monsterAnimation.Collider.Location = monsterAnimation.AnimationPosition = tempPath[counter].IndexesToCorrdinates();
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