using System;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class GiftStateAlive : State
    {
        public override void NextState(Gift context)
        {
            context.CurrentState = new GiftStateDead();
        }

        public override string ToString()
        {
            return "alive";
        }
    }
}