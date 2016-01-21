namespace EscapeRunner.BusinessLogic
{
    public class CollisionEventArgs
    {
        ICollide otherObject;
        public ICollide CollidingObject { get { return otherObject; } }
        public CollisionEventArgs(ICollide collidingObject)
        {
            this.otherObject = collidingObject;
        }
    }
}