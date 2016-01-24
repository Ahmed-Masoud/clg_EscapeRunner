using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic
{
    public class Collider
    {
        private static Dictionary<ICollide, IndexPair> collidables = new Dictionary<ICollide, IndexPair>(8);
        ICollide parentObject;
        public delegate void Colliding(CollisionEventArgs e);

        public event Colliding Collided;
        IndexPair locationIndexes;
        public IndexPair LocationIndexes
        {
            get { return locationIndexes; }
            set
            {
                ICollide another;
                if ((another = CheckCollision()) != null)
                {
                    // Fire the event
                    if (Collided != null)
                        Collided(new CollisionEventArgs(another));
                }
            }
        }
        //public static event
        public Collider(ICollide obj)
        {
            // Set the start location of the collider
            parentObject = obj;
            collidables.Add(obj, obj.ColliderLocationIndexes);
            this.locationIndexes = obj.ColliderLocationIndexes;
        }

        private ICollide CheckCollision()
        {
            // If I'm not colliding with myself or an object of my same type, return that object
            return collidables.Where(p => p.Value == this.parentObject.ColliderLocationIndexes && p.Key.GetType() != this.parentObject.GetType()).First().Key;
        }
    }
}
