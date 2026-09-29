namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// A control systemet enabling triggered behavior in the engine.
    /// Control signals can be raised by commands and functions as interruptable events.
    /// </summary>
    public interface IControlSignal
    {
        /// <summary>
        /// Describe the control signal. This is presented for the user in the editor.
        /// </summary>
        static abstract string Concept { get; }
    }
}