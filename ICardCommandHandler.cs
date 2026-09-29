namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// A handler for a specific type of command that can be executed against the CardCQ engine to perform an action.
    /// Command concepts alter the state of the game and should not return any data. If you need to retrieve data, consider using a query instead.
    /// </summary>
    /// <typeparam name="T">The type of the command which will be handled</typeparam>
    public interface ICardCommandHandler<T> where T : ICardCommand
    {
        /// <summary>
        /// A description of how the command is handled, presented for the card game designer.
        /// </summary>
        static abstract string Description { get; }

        /// <summary>
        /// The actual implementation of the command handling logic, which will be executed when the command is invoked.
        /// </summary>
        /// <param name="command">The command concept with associated arguments</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        Task HandleAsync(T command, ICardEventDispatcher eventDispatcher, CancellationToken ct);
    }
}
