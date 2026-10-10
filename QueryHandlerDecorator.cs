namespace CardCQ.Engine.Abstractions
{
    public abstract class QueryHandlerDecorator<TQuery, TQueryResult>(ICardQueryHandler<TQuery, TQueryResult> decoratee) : ICardQueryHandler<TQuery, TQueryResult>
        where TQuery : ICardQuery<TQueryResult>
    {
        // Settable, so the engine can unlink a decorator below this one
        public ICardQueryHandler<TQuery, TQueryResult> Decoratee { get; set; } = decoratee;

        public virtual Task<TQueryResult> HandleAsync(TQuery query, CancellationToken ct) => Decoratee.HandleAsync(query, ct);
    }
}
