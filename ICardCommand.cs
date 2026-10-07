namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a command that can be executed against the CardCQ engine to perform an action.
    /// A command concept alters the state of the game and should not return any data. If you need to retrieve data, consider using a query instead.
    /// </summary>
    public interface ICardCommand
    {
        /// <summary>
        /// A concept description of the command, presented for the card game designer.
        /// </summary>
        /// <remarks>Virtual rather than abstract so the interface can be used as a generic type argument (e.g. a List&lt;T&gt; of it). Always declare it on the concrete type: the editor reads it from there and shows an empty description otherwise.</remarks>
        static virtual string Concept => string.Empty;
    }
}
