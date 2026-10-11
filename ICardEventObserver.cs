namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines an event observer, which is the only legal place to alter the game state, as events will are stored and replayed to reconstruct the game state.
    /// Observers can only be raised by commands, to comply with the CQRS pattern. 
    /// Observers can be registered for multiple event types, and multiple observers can be registered for the same event type.
    /// </summary>
    /// <typeparam name="T">The type of the card event to observe</typeparam>
    public interface ICardEventObserver<T> where T : ICardEvent
    {
        /// <summary>
        /// Update the game state accordingly and fail critically on exceptions.
        /// Any guard clauses should be handled by the command handler prior to dispatching the event, as observers are not allowed to reject events.
        /// </summary>
        /// <param name="cardEvent">The card event to observe</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
        Task Invoke(T cardEvent, CancellationToken cancellationToken);
    }
}
