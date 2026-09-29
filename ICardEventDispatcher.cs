namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Event dispatcher interface for dispatching events within the CardCQ engine.
    /// The execution scope of the even dispatcher is confined to the CardCQ engine, however, this interface can be mocked and used in unit tests to verify that events are dispatched correctly.
    /// </summary>
    public interface ICardEventDispatcher
    {
        /// <summary>
        /// Method to dispatch an event asynchronously. This method takes an ICardEvent and a CancellationToken, allowing for cancellation of the operation if needed.
        /// </summary>
        /// <param name="cardEvent">Instance of the event to be distributed.</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        Task DispatchAsync(ICardEvent cardEvent, CancellationToken ct);

        /// <summary>
        /// Method to raise a control signal asynchronously. Control signals are used to trigger reaction behavior. Control signals wil not be recorded or replayed to when creating game states.
        /// </summary>
        /// <param name="signal">Obserable control signal</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        Task RaiseSignal(IControlSignal signal, CancellationToken ct);
    }
}
