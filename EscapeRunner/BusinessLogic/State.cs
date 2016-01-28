namespace EscapeRunner.BusinessLogic.GameObjects
{
    abstract class State
    {
        public abstract void NextState(Gift context);
        public abstract override string ToString();
        
    }
}