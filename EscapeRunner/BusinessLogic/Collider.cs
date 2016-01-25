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
        private static Dictionary<ICollide, Rectangle> collidables = new Dictionary<ICollide, Rectangle>(8);
        ICollide parentObject;
        public delegate void Colliding(CollisionEventArgs e);
        private bool colliderActive = false;
        public event Colliding Collided;
        // Start placeholder values to avoid null exceptions
        Rectangle colliderRectangle = Rectangle.Empty;

        public bool ColliderActive
        {
            get { return colliderActive; }
            set
            {
                bool exists = false;

                if (collidables.ContainsKey(this.parentObject))
                    exists = true;

                if (value)
                {
                    // Add to collidables if it doesn't exist
                    if (!exists)
                        collidables.Add(this.parentObject, colliderRectangle);
                }
                else
                {
                    // Remove from list
                    if (exists)
                        collidables.Remove(this.parentObject);
                }
                colliderActive = value;
            }
        }


        public Point Location
        {
            get { return colliderRectangle.Location; }
            set
            {
                colliderRectangle.Location = value;

                // Update value in dictionary
                if (parentObject != null)
                    collidables[parentObject] = colliderRectangle;

                // Check collision
                ICollide another;
                if ((another = CheckCollision()) != null)
                {
                    // Fire the event
                    if (Collided != null)
                        Collided(new CollisionEventArgs(another));
                }
            }
        }

        public Collider(Rectangle colliderInitialRectangle)
        {
            // Placeholder constructor to avoid null exceptions
            this.colliderRectangle = colliderInitialRectangle;
        }

        // I need only the location now, but i'll leave it for simplicity
        public Collider(ICollide obj, Rectangle colliderRectangle)
        {
            // Set the start location of the collider
            if (obj == null)
                throw new NullReferenceException("Parent object is null");
            this.parentObject = obj;
            this.colliderRectangle = colliderRectangle;
            collidables.Add(obj, colliderRectangle);
        }

        private ICollide CheckCollision()
        {
            ICollide retVal = null;

            // If I'm not colliding with myself or an object of my same type, return that object
            if (collidables.Count > 1)
            {
                foreach (var item in collidables)
                {

                    if (this.colliderRectangle.IntersectsWith(item.Value) && item.Key.GetType() != this.parentObject.GetType())
                    {
                        return item.Key;
                    }
                }
                //try
                //{
                //  //  retVal = collidables.Where(p => p.Value.IntersectsWith(this.colliderRectangle) && p.Key.GetType() != this.parentObject.GetType()).First().Key;
                //}
                //catch (InvalidOperationException)
                //{
                //    retVal = null;
                //}
            }
            return retVal;
        }
    }
}
