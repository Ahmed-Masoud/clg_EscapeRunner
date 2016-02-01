using System;
using System.Collections.Generic;
using System.Drawing;

namespace EscapeRunner.BusinessLogic
{
    public class Collider
    {
        // List of all colliders
        private static Dictionary<ICollide, Rectangle> collidables = new Dictionary<ICollide, Rectangle>(8);

        // Parent object holding the collider
        private ICollide parentObject;

        private bool colliderActive = false;

        // Delegate => Method pointer to know the function with the correct signature to recive the 

        public delegate void Colliding(CollisionEventArgs e);

        public event Colliding Collided;

        // Specify collision area
        private Rectangle colliderRectangle;

        /// <summary>
        /// State of the collider is changed to prevent multiple events to be raised during 1 collision
        /// </summary>
        public bool Active
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
            // Collision is checked whenever the location of the collider changes ( object moves )
            set
            {
                colliderRectangle.Location = value;

                // Update value in dictionary
                if (parentObject == null)
                    return;

                collidables[parentObject] = colliderRectangle;
                ICollide another;
                if ((another = CheckCollision()) != null)
                {
                    // Fire the event
                    if (Collided != null)
                    {
                        // Raise the collision event in Both objects
                        Collided(new CollisionEventArgs(another));
                        another.Collider.Collided(new CollisionEventArgs(this.parentObject));
                    }
                }
            }
            get { return colliderRectangle.Location; }
        }

        /// <summary>
        /// Alternate constructor that just makes the collider not null
        /// </summary>
        /// <param name="colliderInitialRectangle"></param>
        public Collider(Rectangle colliderInitialRectangle)
        {
            // Placeholder constructor to avoid null exceptions
            this.colliderRectangle = colliderInitialRectangle;
        }

        /// <summary>
        /// Constructs a real functioning collider
        /// </summary>
        /// <param name="obj">Object holding the collider</param>
        /// <param name="colliderRectangle">Dimensions of the collider</param>
        public Collider(ICollide obj, Rectangle colliderRectangle)
        {
            // Set the start location of the collider
            if (obj == null)
                throw new NullReferenceException("Parent object is null");

            this.parentObject = obj;
            this.colliderRectangle = colliderRectangle;

            // Set the collider to active and check collision
            this.Active = true;
        }

        private ICollide CheckCollision()
        {
            ICollide retVal = null;

            // If I'm not colliding with myself or an object of my same type, return that object
            if (collidables.Count > 1)
            {
                foreach (var item in collidables)
                {
                    if (item.Key.GetType() != this.parentObject.GetType()   // Objects of the same type don't collide
                        && this.colliderRectangle.IntersectsWith(item.Value)) // If there is a collision
                    {
                        // Return the parent object of the colliding item
                        return item.Key;
                    }
                }
            }
            return retVal;
        }
    }
}