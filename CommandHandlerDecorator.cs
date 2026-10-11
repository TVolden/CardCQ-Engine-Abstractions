namespace CardCQ.Engine.Abstractions
{
    public abstract class CommandHandlerDecorator<TCommand>(ICardCommandHandler<TCommand> decoratee) : ICardCommandHandler<TCommand>
        where TCommand : ICardCommand
    {
        /// <summary>
        /// The command handler that this decorator wraps. This property is set in the constructor and should not be changed after that.
        /// </summary>
        public ICardCommandHandler<TCommand> Decoratee { get; set; } = decoratee;

        /// <summary>
        /// Handles the command by calling the BeforeHandleAsync method, then the decoratee's HandleAsync method, and finally the AfterHandleAsync method.
        /// Override this method to manually control the order of the execution, or to manipulate the command or event dispatcher before or after the decoratee is called.
        /// Remember to manually call the before/after methods if needed, and to call the decoratee 's HandleAsync method to ensure the command is handled.
        /// </summary>
        /// <param name="command">Command which the decorator will process</param>
        /// <param name="eventDispatcher">Event dispatcher to use for dispatching events</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        public virtual async Task HandleAsync(TCommand command, ICardEventDispatcher eventDispatcher, CancellationToken ct)
        {
            await BeforeHandleAsync(command, eventDispatcher, ct);
            await Decoratee.HandleAsync(command, eventDispatcher, ct);
            await AfterHandleAsync(command, eventDispatcher, ct);
        }

        /// <summary>
        /// Called before the decoratee's HandleAsync method is called. Override this method to add additional behavior before the decoratee is called.
        /// </summary>
        /// <param name="command">The command which is being handled</param>
        /// <param name="eventDispatcher">The event dispatcher to use for dispatching events</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        public virtual async Task BeforeHandleAsync(TCommand command, ICardEventDispatcher eventDispatcher, CancellationToken ct) { }

        /// <summary>
        /// Called after the decoratee's HandleAsync method is called. Override this method to add additional behavior after the decoratee is called.
        /// </summary>
        /// <param name="command">The command which is being handled</param>
        /// <param name="eventDispatcher">The event dispatcher to use for dispatching events</param>
        /// <param name="ct"><see cref="CancellationToken"/></param>
        public virtual async Task AfterHandleAsync(TCommand command, ICardEventDispatcher eventDispatcher, CancellationToken ct) { }
    }
}
