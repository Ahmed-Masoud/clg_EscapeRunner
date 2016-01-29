namespace EscapeRunner.BusinessLogic
{
    public class CollisionEventArgs
    {
        private ICollide otherObject;
        public ICollide CollidingObject { get { return otherObject; } }

        public CollisionEventArgs(ICollide collidingObject)
        {
            this.otherObject = collidingObject;
        }
    }
}