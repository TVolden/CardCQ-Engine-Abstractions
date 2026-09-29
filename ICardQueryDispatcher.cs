namespace CardCQ.Engine.Abstractions
{
    public interface ICardQueryDispatcher
    {
        Task<T> DispatchAsync<T>(ICardQuery<T> cardEvent, CancellationToken ct);
    }
}