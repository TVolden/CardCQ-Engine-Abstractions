namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// A signal observed by a card rather than by the game: it runs the <c>when</c> blocks of the card named
    /// <see cref="CardName"/> (a <c>card "Name":</c> block of the script). A package raises it once for each card instance it
    /// concerns, and carries the instance, so the card's behavior can read it as a signal parameter.
    /// Plain <see cref="IGameSignal"/>s trigger general game behavior once and are not tied to cards.
    /// </summary>
    public interface ICardSignal
    {
        /// <summary>
        /// Describe the card signal. This is presented for the user in the editor.
        /// </summary>
        static virtual string Concept => string.Empty;

        /// <summary>The card whose behavior the signal runs, by its name in the project.</summary>
        string CardName => throw new NotSupportedException(
            $"{GetType().Name} doesn't name the card it is for, so no card can react to it. Build its package against CardCQ.Engine.Abstractions 0.8.0 or later, and implement {nameof(CardName)}.");
    }
}
