using System.Windows.Input;

namespace CardCQ.Engine.Abstractions
{
    public interface ICQService
    {
        void RegisterCommandHandler<TCommand, TCommandHandler>() where TCommand : ICardCommand where TCommandHandler : ICardCommandHandler<TCommand>;
        void RegisterQueryHandler<TQuery, TQueryResult, TQueryHandler>() where TQuery : ICardQuery<TQueryResult> where TQueryHandler : ICardQueryHandler<TQuery, TQueryResult>;
    }
}
