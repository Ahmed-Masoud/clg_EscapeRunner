using EscapeRunner.Animations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace EscapeRunner
{
    internal abstract class MovingObject
    {
        public enum Direction { up, down, left, right };

        public abstract void Draw(Graphics g);

        public abstract void Move(Direction direction);

        public abstract bool CanMove(Direction direction);
    }

    internal class Hero : MovingObject
    {
        private static Bitmap texture;
        private static Point location;
        private static int dx;
        private static int dy;
        private static Rectangle hiddenRectangle;
        private static Hero player;
        private static int leftImageCode;
        private static int rightImageCode;
        public static List<Image> motionR = new List<Image>();
        public static List<Image> motionL = new List<Image>();
        public static Direction direction;

        private Hero()
        {
            leftImageCode = 0; rightImageCode = 0;
        }

        public static Hero Player
        {
            get
            {
                if (player == null)
                    player = new Hero();
                return player;
            }
        }

        public static Bitmap Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public static Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public static Rectangle BorderRectangle
        {
            get { return hiddenRectangle; }
            set { hiddenRectangle = value; }
        }

        public static int Dx
        {
            get { return dx; }
            set { dx = value; }
        }

        public static int Dy
        {
            get { return dy; }
            set { dy = value; }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(texture, hiddenRectangle);
        }

        public override void Move(Direction direction)
        {
            if (CanMove(direction))
            {
                if (direction == Direction.up)
                {
                    location.Y -= dy;
                    Hero.direction = direction;
                    hiddenRectangle.Y = location.Y;
                    leftImageCode = 0; rightImageCode = 0;
                }
                else if (direction == Direction.down)
                {
                    location.Y += dy;
                    Hero.direction = direction;
                    hiddenRectangle.Y = location.Y;
                    leftImageCode = 0; rightImageCode = 0;
                }
                else if (direction == Direction.left)
                {
                    location.X -= dx;
                    Hero.direction = direction;
                    //if (playerImageCode == 7)
                    //    playerImageCode = 0;
                    texture = (Bitmap)motionL[leftImageCode++ % 7];
                    hiddenRectangle.X = location.X;
                    rightImageCode = 0;
                }
                else if (direction == Direction.right)
                {
                    location.X += dx;
                    Hero.direction = direction;
                    texture = (Bitmap)motionR[rightImageCode++ % 7];
                    hiddenRectangle.X = location.X;
                    leftImageCode = 0;
                }
            }
        }

        public override bool CanMove(Direction direction)
        {
            if (direction == Direction.up)
            {
                if (location.Y - dy >= 0)
                    return true;
                return false;
            }
            else if (direction == Direction.down)
            {
                if (location.Y + dy <= MainWindow.LowerBound)
                    return true;
                return false;
            }
            else if (direction == Direction.left)
            {
                if (location.X - dx >= 0)
                    return true;
                return false;
            }
            else if (direction == Direction.right)
            {
                if (location.X + dx <= MainWindow.RightBound)
                    return true;
                return false;
            }
            return false;
        }
    }

    internal class Bullet : MovingObject
    {
        private static Bitmap texture;
        private Point location;
        private static int dx;
        private static int dy;
        private Rectangle hiddenRectangle;
        public bool isShotInAir;
        public bool isShotPrinted;

        public Bullet()
        {
            isShotInAir = false;
            isShotPrinted = false;
            if (Player.Direction == Directions.Right)
            {
                location = new Point(Hero.Location.X - 20, Hero.Location.Y + 20);
                hiddenRectangle = new Rectangle(Hero.Location.X - 20, Hero.Location.Y + 20, 10, 10);
            }
            else if (Player.Direction == Directions.Left)
            {
                location = new Point(Hero.Location.X + 50, Hero.Location.Y + 20);
                hiddenRectangle = new Rectangle(Hero.Location.X + 50, Hero.Location.Y + 20, 10, 10);
            }
        }

        public static Bitmap Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public Rectangle BorderRectangle
        {
            get { return hiddenRectangle; }
            set { hiddenRectangle = value; }
        }

        public static int Dx
        {
            get { return dx; }
            set { dx = value; }
        }

        public static int Dy
        {
            get { return dy; }
            set { dy = value; }
        }

        public override bool CanMove(Direction direction)
        {
            if (direction == Direction.left)
            {
                if (location.X >= 0)
                    return true;
                return false;
            }
            else if (direction == Direction.right)
            {
                if (location.X <= MainWindow.RightBound)
                    return true;
                return false;
            }
            else if (direction == Direction.up)
            {
                if (location.Y >= 0)
                    return true;
                return false;
            }
            else if (direction == Direction.down)
            {
                if (location.Y <= MainWindow.LowerBound)
                    return true;
                return false;
            }
            return false;
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(texture, hiddenRectangle);
        }

        public override void Move(Direction direction)
        {
            if (CanMove(direction))
            {
                if (direction == Direction.right)
                {
                    Task.Run(() =>
                    {
                        while (CanMove(Direction.left))
                        {
                            location.X -= dx;
                            hiddenRectangle.X = location.X;
                            isShotInAir = true;
                            Thread.Sleep(70);
                        }
                        isShotInAir = false;
                    });
                }
                else if (direction == Direction.left)
                {
                    Task.Run(() =>
                    {
                        while (CanMove(Direction.right))
                        {
                            location.X += dx;
                            hiddenRectangle.X = location.X;
                            isShotInAir = true;
                            Thread.Sleep(70);
                        }
                        isShotInAir = false;
                    });
                }
            }
        }
    }

    internal class Fart : MovingObject
    {
        private Bitmap texture;
        private static Point location;
        private Rectangle hiddenRectangle;
        public int fartImageCode;
        public static List<Image> fartL = new List<Image>();
        public static List<Image> fartR = new List<Image>();

        public Fart()
        {
            fartImageCode = 0;
            hiddenRectangle.Width = 20;
            hiddenRectangle.Height = 20;
            if (Hero.direction == Direction.right)
            {
                hiddenRectangle.X = Hero.Location.X - 20;
                hiddenRectangle.Y = Hero.Location.Y + 20;
            }
            else if (Hero.direction == Direction.left)
            {
                hiddenRectangle.X = Hero.Location.X + 50;
                hiddenRectangle.Y = Hero.Location.Y + 20;
            }
        }

        public Bitmap Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public static Point Location
        {
            get { return location; }
            set { location = value; }
        }

        public Rectangle BorderRectangle
        {
            get { return hiddenRectangle; }
            set { hiddenRectangle = value; }
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(texture, hiddenRectangle);
        }

        public void shoot()
        {
            if (Hero.direction == Direction.right)
            {
                Task.Run(() =>
                {
                    for (int i = 0; i < 11; i++)
                    {
                        texture = (Bitmap)fartR[fartImageCode++];
                        Thread.Sleep(100);
                    }
                    fartImageCode = 0;
                });
            }
            else if (Hero.direction == Direction.left)
            {
                Task.Run(() =>
                {
                    for (int i = 0; i < 11; i++)
                    {
                        texture = (Bitmap)fartL[fartImageCode++];
                        Thread.Sleep(100);
                    }
                    fartImageCode = 0;
                });
            }
        }

        public override void Move(Direction direction)
        {
            throw new NotImplementedException();
        }

        public override bool CanMove(Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}