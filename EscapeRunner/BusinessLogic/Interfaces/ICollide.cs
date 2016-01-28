namespace EscapeRunner.BusinessLogic
{
    public interface ICollide
    {
        // Used for adapter design pattern to implement collision

        /* The ICollide object provides the rectangle for the collider */

        /// <summary>
        /// Location indexes are used to keep the collider updated with the parent object's location
        /// </summary>
        Collider Collider { get; }
    }
}