using System;

namespace EscapeRunner.BusinessLogic
{
    public interface ICollide
    {
        // Used for adapter design pattern to implement collision

        /// <summary>
        /// Location indexes are used to keep the collider updated with the parent object's location
        /// </summary>
        IndexPair ColliderLocationIndexes { get; set; }
        Collider Collider { get; }
    }
}