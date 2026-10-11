namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a decorator for a query handler that can be used to add additional behavior to the handling of queries in the CardCQ engine.
    /// </summary>
    /// <typeparam name="TQuery">The query concept which is handled</typeparam>
    /// <typeparam name="TQueryResult">The type of the result of the query handling</typeparam>
    /// <param name="decoratee">The query handler to be decorated</param>
    public abstract class QueryHandlerDecorator<TQuery, TQueryResult>(ICardQueryHandler<TQuery, TQueryResult> decoratee) : ICardQueryHandler<TQuery, TQueryResult>
        where TQuery : ICardQuery<TQueryResult>
    {
        /// <summary>
        /// The query handler that is being decorated by this decorator.
        /// This property allows the engine to remove the decorator and restore the original query handler if needed.
        /// Do not change this property directly, but use the <see cref="ICardCQService"/> to change the query handler.
        /// </summary>
        public ICardQueryHandler<TQuery, TQueryResult> Decoratee { get; set; } = decoratee;

        /// <summary>
        /// Calls the BeforeHandleAsync method, then the decoratee's HandleAsync method, and finally the AfterHandleAsync method.
        /// Override this method to manually control the order of the execution, or to manipulate the query before the decoratee is called or the result after the decoratee is called.
        /// Remember to manually call the before/after methods if needed, and to call the decoratee's HandleAsync method to ensure the query is handled.
        /// </summary>
        /// <param name="query">The query to handle</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        /// <returns>The result of the query handling</returns>
        public virtual async Task<TQueryResult> HandleAsync(TQuery query, CancellationToken ct) 
        { 
            await BeforeHandleAsync(query, ct);
            var result = await Decoratee.HandleAsync(query, ct);
            return await AfterHandleAsync(query, result, ct);
        }

        /// <summary>
        /// Called before the decoratee's HandleAsync method is called. Override this method to add additional behavior before the decoratee is called.
        /// </summary>
        /// <param name="query">The query to handle</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        public virtual async Task BeforeHandleAsync(TQuery query, CancellationToken ct) { }

        /// <summary>
        /// Called after the decoratee's HandleAsync method is called. Override this method to add additional behavior after the decoratee is called.
        /// </summary>
        /// <param name="query">The query that was handled</param>
        /// <param name="result">The result of the decorated query handling</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        /// <returns>The final result of the query handling</returns>
        public virtual async Task<TQueryResult> AfterHandleAsync(TQuery query, TQueryResult result, CancellationToken ct) => result;
    }
}
