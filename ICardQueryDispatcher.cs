namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a dispatcher that can handle queries in the CardCQ engine.
    /// This allows queries to fetch data while ensuring that the state of the game remains unchanged.
    /// </summary>
    public interface ICardQueryDispatcher
    {
        /// <summary>
        /// Dispatches a query to the appropriate handler for execution, returning the result of type T.
        /// </summary>
        /// <typeparam name="T">The result type of the query</typeparam>
        /// <param name="query">The card query to dispatch</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>The result of the query</returns>
        Task<T> DispatchAsync<T>(ICardQuery<T> query, CancellationToken ct);
    }
}