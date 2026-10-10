namespace CardCQ.Engine.Abstractions
{
    public abstract class CommandHandlerDecorator<TCommand>(ICardCommandHandler<TCommand> decoratee) : ICardCommandHandler<TCommand>
        where TCommand : ICardCommand
    {
        // Settable, so the engine can unlink a decorator below this one
        public ICardCommandHandler<TCommand> Decoratee { get; set; } = decoratee;

        public virtual Task HandleAsync(TCommand command, ICardEventDispatcher eventDispatcher, CancellationToken ct)
            => Decoratee.HandleAsync(command, eventDispatcher, ct);
    }
}
