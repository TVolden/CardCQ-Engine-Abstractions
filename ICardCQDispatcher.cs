namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a dispatcher that can handle both commands and queries in the CardCQ engine.
    /// </summary>
    public interface ICardCQDispatcher : ICardQueryDispatcher
    {
        /// <summary>
        /// Dispatches a command to the appropriate handler for execution, which may result in events being dispatched.
        /// </summary>
        /// <param name="command">An instantiated command concept to be dispatched and handled</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        Task DispatchAsync(ICardCommand command, CancellationToken ct);
    }
}
