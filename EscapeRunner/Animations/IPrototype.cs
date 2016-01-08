namespace EscapeRunner.Animations
{
    /// <summary>
    /// Represents a generic prototype pattern interface
    /// </summary>
    internal interface IPrototype<T>
    {
        #region Public Methods

        // The interface is used for explosion animations
        T Clone();

        #endregion
    }
}