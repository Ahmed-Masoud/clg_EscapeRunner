using EscapeRunner.Animations;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class Monster : IDrawable
    {
        private MonsterAnimation monsterAnimation;

        private List<IndexPair> monsterPath;
        private System.Timers.Timer makeStepTimer = new System.Timers.Timer();
        private bool increasing;
        private int counter;

        // TODO change to state
        public bool Alive { get; set; }
        public int ZOrder { get; set; } = 4;
        public Monster(IndexPair startPoint, IndexPair endPoint)
        {
            increasing = true;
            counter = 0;
            AnimationFactory factory = new AnimationFactory();
            PathFinder myPath = new PathFinder(new RouteInformation(startPoint, endPoint));
            monsterPath = myPath.FindPath();

            makeStepTimer.Elapsed += Timer_Elapsed;
            makeStepTimer.Enabled = true;
            makeStepTimer.Interval = 250;

            Alive = true;

            monsterAnimation = (MonsterAnimation)factory.CreateAnimation(AnimationType.MonsterAnimation, monsterPath[0]);
            monsterAnimation.AnimationPosition = startPoint.IndexesToCoordinates();

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

            // Change Z order when monster moves
            this.ZOrder = Position.X + Position.Y;
        }

        private void Monster_Collided(CollisionEventArgs e)
        {
            // Game over
            if (e.CollidingObject.ToString().Equals("player"))
            {
                Task.Run(() => AudioController.PlayExplosion());

                Controller.GameOver(1);
                monsterAnimation.Collider.Active = false;
            }
            else if (e.CollidingObject.ToString().Equals("bullet"))
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
            monsterAnimation.Collider.Location = monsterAnimation.AnimationPosition = monsterPath[counter].IndexesToCoordinates();
        }

        /// <summary>
        /// Counts the series 0...n.n-1...0 repeat for the monster's Path
        /// </summary>
        private void NextStepIndex()
        {
            if (increasing)
            {
                if (counter++ == monsterPath.Count - 1)
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