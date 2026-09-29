namespace CardCQ.Engine.Abstractions
{
    public interface ICardEventObserver<T> where T : ICardEvent
    {
        Task Invoke(T cardEvent, CancellationToken cancellationToken);
    }
}
