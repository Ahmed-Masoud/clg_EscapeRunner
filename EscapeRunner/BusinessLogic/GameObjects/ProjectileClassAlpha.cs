﻿using EscapeRunner.Animations;
using System;
using System.Drawing;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    /// <summary>
    /// Projectiles are implemented using object pool design pattern
    /// </summary>
    public sealed class ProjectileClassAlpha : IWeapon, IDrawable
    {
        private static BulletAnimation prototypeBulletAnimation =
            (BulletAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.BulletAnimation);

        private static ExplosionAnimation prototypeExplosionAnimation =
            (ExplosionAnimation)AnimationFactory.CreateEmpyAnimation(AnimationType.ExplosionAnimation);

        private BulletAnimation bulletAni;
        private Point bulletStartPosition = Point.Empty;
        private Point explosionPosition = Point.Empty;
        private ExplosionAnimation explosionAni;   // Change ExplosionAnimation class to public to make this work

        private bool used;

        internal ProjectileClassAlpha()
        {
            // Used for lazy initialization in the bullet pool
            explosionAni = (ExplosionAnimation)((IPrototype<Animation>)prototypeExplosionAnimation).Clone();
            bulletAni = (BulletAnimation)((IPrototype<Animation>)prototypeBulletAnimation).Clone();

            explosionAni.AnimationPosition = Point.Empty;
            bulletAni.AnimationPosition = Point.Empty;
            bulletAni.Collider.Collided += BulletCollider_Collided;
        }

        private void ProjectileAlpha_Collided(CollisionEventArgs e)
        {
            // TODO collision check for monster and remove it
            //if (e.CollidingObject is Monster)
            //AudioController.();
        }

        private void BulletCollider_Collided(CollisionEventArgs e)
        {
            if (e.CollidingObject.ToString() == "player")
                return;
            // Check the colliding object, and do the bullet's reactions
            //if (e.CollidingObject is Monster)
            //  System.Diagnostics.Debug.WriteLine("Collided with a monster");
            System.Diagnostics.Debug.WriteLine("Collision detected from bullet to " + e.CollidingObject.GetType().ToString());
            bulletAni.Collider.ColliderActive = false;

            // Setting the usage of bullet to false
            bulletAni.Visible = false;
        }

        public Point BulletStartPosition
        {
            set { bulletStartPosition = bulletAni.AnimationPosition = value; }
            get { return bulletStartPosition; }
        }

        public Point ExplosionPosition
        {
            set { explosionPosition = explosionAni.AnimationPosition = value; }
            get { return explosionPosition; }
        }

        public bool Used
        {
            get
            { return used; }
            set
            {
                if (value == true)
                {
                    bulletAni.Collider.ColliderActive = true;
                    bulletAni.Visible = true;
                }
                else
                {
                    // Deactivate the collider to avoid keeping the bullet's collider when it's not active
                    bulletAni.Collider.ColliderActive = false;
                    bulletAni.Locked = false;
                }
                used = value;
            }
        }

        public Point DrawLocation
        {
            get
            {
                return bulletAni.AnimationPosition;
            }
        }

        public void UpdateGraphics(Graphics g)
        {
            bulletAni.DrawBullet(g);
            UpdateShotState();
        }

        private void UpdateShotState()
        {
            if (bulletAni.Visible == false)
                this.Used = false;
        }
    }
}