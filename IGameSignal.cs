namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// A game signal enabling triggered behavior in the engine.
    /// Game signals can be raised by commands and functions as interruptable events.
    /// </summary>
    public interface IGameSignal
    {
        /// <summary>
        /// Describe the game signal. This is presented for the user in the editor.
        /// </summary>
        static virtual string Concept => string.Empty;
    }
}