using EscapeRunner.Animations;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class Monster : IDrawable
    {
        private int pathCounter;
        private bool patrolPathUp;
        private System.Timers.Timer makeStepTimer = new System.Timers.Timer();
        private MonsterAnimation monsterAnimation;

        private List<IndexPair> monsterPath;

        // TODO change to state
        public bool Alive { get; set; }

        public Directions Direction { get; set; }

        public Point DrawLocation { get { return monsterAnimation.AnimationPosition; } }

        public int ZOrder { get; set; } = 4;

        public Monster(IndexPair startPoint, IndexPair endPoint)
        {
            patrolPathUp = true;
            pathCounter = 0;
            AnimationFactory factory = new AnimationFactory();

            PathFinder pathFinder = new PathFinder(new RouteInformation(startPoint, endPoint));
            monsterPath = pathFinder.FindPath();

            makeStepTimer.Elapsed += Timer_Elapsed;
            makeStepTimer.Enabled = true;
            makeStepTimer.Interval = 250;

            Alive = true;

            monsterAnimation = (MonsterAnimation)factory.CreateAnimation(AnimationType.MonsterAnimation, monsterPath[0]);
            monsterAnimation.AnimationPosition = startPoint.IndexesToCoordinates();

            // TODO set Monster start position at game start. TODO set Monster Direction at game start.
            monsterAnimation.Collider.Collided += Monster_Collided;

            pathFinder = null;  // Release object
        }

        public void UpdateGraphics(Graphics g)
        {
            if (Alive)
            {
                monsterAnimation.Draw(g, Direction);
                // Update the collider's location
                monsterAnimation.Collider.Location = monsterAnimation.AnimationPosition = monsterPath[pathCounter].IndexesToCoordinates();
            }
            else
            {
                // Remove monster from collision detection
                monsterAnimation.Collider.Active = false;
            }

        }

        private void GraphicsSynchronizationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            monsterAnimation.LoadNextAnimationImage();
        }

        private void Monster_Collided(CollisionEventArgs e)
        {
            // Game over
            if (e.CollidingObject.ToString().Equals("player"))
            {
                monsterAnimation.Collider.Active = false;

                Task.Run(() => AudioController.PlayExplosion());
                Controller.GameOver(1);
            }
            else if (e.CollidingObject.ToString().Equals("bullet"))
            {
                monsterAnimation.Collider.Active = false;
                Alive = false;

                Controller.Score += 20; // Add score
                AudioController.PlayMonsterDieSound();
            }
        }

        /// <summary>
        /// Counts the series 0...n.n-1...0 repeat for the monster's Path
        /// </summary>
        private void NextStepIndex()
        {
            if (patrolPathUp)
            {
                if (pathCounter++ == monsterPath.Count - 1)
                {
                    patrolPathUp = false;
                    pathCounter -= 2;
                }
            }
            else
            {
                if (--pathCounter == -1)
                {
                    patrolPathUp = true;
                    pathCounter += 2;
                }
            }
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            NextStepIndex();

            // Change Z order when monster moves
            this.ZOrder = DrawLocation.X + DrawLocation.Y;
        }
    }
}