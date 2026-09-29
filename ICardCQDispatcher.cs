namespace CardCQ.Engine.Abstractions
{
    public interface ICardCQDispatcher : ICardQueryDispatcher
    {
        Task DispatchAsync(ICardCommand cardEvent, CancellationToken ct);
    }
}
