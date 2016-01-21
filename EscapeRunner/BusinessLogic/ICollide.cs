using System;

namespace EscapeRunner.BusinessLogic
{
    public interface ICollide
    {
        // Used for adapter design pattern to implement collision
        IndexPair LocationIndexes { get; set; }
        Collider collider { set; }
    }
}