using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRunner.BusinessLogic.GameObjects
{
    class GiftStateDead : State
    {
        public override void NextState(Gift context)
        {
            context.CurrentState = new GiftStateAlive();
        }

        public override string ToString()
        {
            return "dead";
        }
    }
}
