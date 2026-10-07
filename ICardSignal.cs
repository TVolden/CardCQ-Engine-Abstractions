namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// A signal observed by cards rather than by the game.
    /// A card is instantiated when it is added to a collection, and a raised card signal triggers each instance of the cards observing it.
    /// Plain <see cref="IGameSignal"/>s trigger general game behavior once and are not tied to card instances.
    /// </summary>
    public interface ICardSignal;
}
