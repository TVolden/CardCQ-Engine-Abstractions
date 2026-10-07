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
        /// <remarks>Virtual rather than abstract so the interface can be used as a generic type argument (e.g. a List&lt;T&gt; of it). Always declare it on the concrete type: the editor reads it from there and shows an empty description otherwise.</remarks>
        static virtual string Concept => string.Empty;
    }
}