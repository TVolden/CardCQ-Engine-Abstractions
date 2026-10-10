namespace CardCQ.Engine.Abstractions
{
    public interface ICardCQService
    {
        ICardCQDispatcher Dispatcher { get; }

        /// <summary>Replaces an existing handler, keeping its decorators.</summary>
        void RegisterCommandHandler<TCommand>(ICardCommandHandler<TCommand> handler) where TCommand : ICardCommand;

        /// <summary>Replaces an existing handler, keeping its decorators.</summary>
        void RegisterQueryHandler<TQuery, TQueryResult>(ICardQueryHandler<TQuery, TQueryResult> handler) where TQuery : ICardQuery<TQueryResult>;

        void RegisterEventObserver<TEvent>(ICardEventObserver<TEvent> observer) where TEvent : ICardEvent;

        void RegisterSignalObserver<TSignal>(IGameSignalObserver<TSignal> observer) where TSignal : IGameSignal;

        void AddCommandDecorator<TCommand, TDecorator>(Func<ICardCommandHandler<TCommand>, TDecorator> decorate)
            where TCommand : ICardCommand where TDecorator : CommandHandlerDecorator<TCommand>;

        bool RemoveCommandDecorator<TCommand, TDecorator>() where TCommand : ICardCommand where TDecorator : CommandHandlerDecorator<TCommand>;

        void AddQueryDecorator<TQuery, TQueryResult, TDecorator>(Func<ICardQueryHandler<TQuery, TQueryResult>, TDecorator> decorate)
            where TQuery : ICardQuery<TQueryResult> where TDecorator : QueryHandlerDecorator<TQuery, TQueryResult>;

        bool RemoveQueryDecorator<TQuery, TQueryResult, TDecorator>()
            where TQuery : ICardQuery<TQueryResult> where TDecorator : QueryHandlerDecorator<TQuery, TQueryResult>;
    }
}
