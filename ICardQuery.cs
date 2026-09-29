namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a query that can be executed against the CardCQ engine to retrieve data of type T.
    /// A query should not alter the state of the game and is intended solely for data retrieval. 
    /// If you need to perform an action that changes the game state, consider using a command instead.
    /// </summary>
    /// <typeparam name="T">The return value for the type</typeparam>
    public interface ICardQuery<T>
    {
        /// <summary>
        /// A concept description of the query, presented for the card game designer.
        /// </summary>
        static abstract string Concept { get; }
    }
}
