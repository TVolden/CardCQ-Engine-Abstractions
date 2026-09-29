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
        static abstract string Concept { get; }
    }
}
