namespace EscapeRunner.BusinessLogic.GameObjects
{
    internal abstract class State
    {
        public abstract void NextState(Gift context);

        public abstract override string ToString();
    }
}