namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Registers a package's handlers, decorators and observers with the engine.
    /// Every registration method is a command: it changes the engine and returns nothing.
    /// </summary>
    public interface ICardCQService
    {
        ICardCQDispatcher Dispatcher { get; }

        /// <summary>
        /// Replaces an existing handler, keeping its decorators.
        /// </summary>
        /// <typeparam name="TCommand">The command concept which the handler will process</typeparam>
        /// <param name="handler">An instance of the command handler to register</param>
        void RegisterCommandHandler<TCommand>(ICardCommandHandler<TCommand> handler) where TCommand : ICardCommand;

        /// <summary>
        /// Replaces an existing handler, keeping its decorators.
        /// </summary>
        /// <typeparam name="TQuery">The query concept which the handler will process</typeparam>
        /// <typeparam name="TQueryResult">The result type of the query</typeparam>
        /// <param name="handler">An instance of the query handler to register</param>
        void RegisterQueryHandler<TQuery, TQueryResult>(ICardQueryHandler<TQuery, TQueryResult> handler) where TQuery : ICardQuery<TQueryResult>;

        /// <summary>
        /// Registers an event observer to update the game state when events are dispatched. 
        /// Observers can be registered for multiple event types, and multiple observers can be registered for the same event type.
        /// </summary>
        /// <typeparam name="TEvent">The event concept which the observer will process</typeparam>
        /// <param name="observer">An instance of the event observer to register</param>
        void RegisterEventObserver<TEvent>(ICardEventObserver<TEvent> observer) where TEvent : ICardEvent;

        /// <summary>
        /// Register a signal observer to react to signals dispatched by the game engine. 
        /// Observers can be registered for multiple signal types, and multiple observers can be registered for the same signal type.
        /// </summary>
        /// <typeparam name="TSignal">The signal concept which the observer will process</typeparam>
        /// <param name="observer">An instance of the signal observer to register</param>
        void RegisterSignalObserver<TSignal>(IGameSignalObserver<TSignal> observer) where TSignal : IGameSignal;

        /// <summary>
        /// Add a decorator to an existing command handler, allowing for additional behavior to be added to the command handling process.
        /// </summary>
        /// <typeparam name="TCommand">The command concept which the decorator will process</typeparam>
        /// <typeparam name="TDecorator">The decorator concept which will wrap the command handler</typeparam>
        /// <param name="decorate">A function that takes the original command handler and returns the decorated version</param>
        void AddCommandDecorator<TCommand, TDecorator>(Func<ICardCommandHandler<TCommand>, TDecorator> decorate)
            where TCommand : ICardCommand where TDecorator : CommandHandlerDecorator<TCommand>;

        /// <summary>
        /// Removes a decorator from an existing command handler, allowing for the original command handling behavior to be restored.
        /// Does nothing if the decorator has not been added.
        /// </summary>
        /// <typeparam name="TCommand">The command concept which the decorator will process</typeparam>
        /// <typeparam name="TDecorator">The decorator concept which will wrap the command handler</typeparam>
        void RemoveCommandDecorator<TCommand, TDecorator>() where TCommand : ICardCommand where TDecorator : CommandHandlerDecorator<TCommand>;

        /// <summary>
        /// Add a decorator to an existing query handler, allowing for additional behavior to be added to the query handling process.
        /// </summary>
        /// <typeparam name="TQuery">The query concept which the decorator will process</typeparam>
        /// <typeparam name="TQueryResult">The result concept which the decorator will process</typeparam>
        /// <typeparam name="TDecorator">The decorator concept which will wrap the query handler</typeparam>
        /// <param name="decorate">A function that takes the original query handler and returns the decorated version</param>
        void AddQueryDecorator<TQuery, TQueryResult, TDecorator>(Func<ICardQueryHandler<TQuery, TQueryResult>, TDecorator> decorate)
            where TQuery : ICardQuery<TQueryResult> where TDecorator : QueryHandlerDecorator<TQuery, TQueryResult>;

        /// <summary>
        /// Removes a decorator from an existing query handler, allowing for the original query handling behavior to be restored.
        /// Does nothing if the decorator has not been added.
        /// </summary>
        /// <typeparam name="TQuery">The query concept which the decorator will process</typeparam>
        /// <typeparam name="TQueryResult">The result concept which the decorator will process</typeparam>
        /// <typeparam name="TDecorator">The decorator concept which will wrap the query handler</typeparam>
        void RemoveQueryDecorator<TQuery, TQueryResult, TDecorator>()
            where TQuery : ICardQuery<TQueryResult> where TDecorator : QueryHandlerDecorator<TQuery, TQueryResult>;
    }
}
