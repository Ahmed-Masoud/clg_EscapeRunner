namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal class GiftStateDead : State
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