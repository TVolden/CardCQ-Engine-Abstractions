namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Custom handler for a specific type of query that can be executed against the CardCQ engine to retrieve data of type RT.
    /// This handler is not provided an instance of the IEventDispatcher, as queries should not alter the state of the game and should only be used for data retrieval.
    /// </summary>
    /// <typeparam name="T">The query which the handler handles</typeparam>
    /// <typeparam name="RT">The returned value of the query</typeparam>
    public interface ICardQueryHandler<T, RT> where T : ICardQuery<RT>
    {
        /// <summary>
        /// A description of how the query is handled, presented for the card game designer.
        /// </summary>
        static abstract string Description { get; }

        /// <summary>
        /// Handles the query and returns the result of type RT. This method is asynchronous and can be cancelled using the provided CancellationToken.
        /// </summary>
        /// <param name="query">The query concept which is handled</param>+
        /// <param name="ct"><see cref="CancellationToken"/></param>
        /// <returns></returns>
        Task<RT> HandleAsync(T query, CancellationToken ct);
    }
}
