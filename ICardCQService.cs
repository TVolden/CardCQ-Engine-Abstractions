namespace CardCQ.Engine.Abstractions
{
    public interface ICardCQService
    {
        void RegisterCommandHandler<TCommand, TCommandHandler>() where TCommand : ICardCommand where TCommandHandler : ICardCommandHandler<TCommand>;
        void RegisterQueryHandler<TQuery, TQueryResult, TQueryHandler>() where TQuery : ICardQuery<TQueryResult> where TQueryHandler : ICardQueryHandler<TQuery, TQueryResult>;
        void RegisterEventObserver<TEvent, TEventObserver>() where TEvent : ICardEvent where TEventObserver : ICardEventObserver<TEvent>;
        void RegisterSignalObserver<TSignal, TSignalObserver>() where TSignal : IControlSignal where TSignalObserver : IControlSignalObserver<TSignal>;
    }
}
