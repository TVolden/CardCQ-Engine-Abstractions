namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a custom type that code blocks may take as a parameter or return from a query, such as a card or a collection.
    /// Built-in types (string, int, bool, ...) need no marker.
    /// </summary>
    public interface IBlockValueType
    {
        /// <summary>
        /// A concept description of the value type, presented for the card game designer.
        /// </summary>
        /// <remarks>Virtual rather than abstract so the interface can be used as a generic type argument (e.g. a List&lt;T&gt; of it). Always declare it on the concrete type: the editor reads it from there and shows an empty description otherwise.</remarks>
        static virtual string Concept => string.Empty;
    }
}
