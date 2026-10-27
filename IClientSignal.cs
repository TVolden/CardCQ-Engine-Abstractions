namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a game signal that carries nothing hidden from the players (no card identity, no instance ids), so a game host
    /// may pass it on to every player's client, e.g. to show a transition. Unmarked signals stay inside the engine.
    /// The engine doesn't check the marker; a host decides by it what reaches the clients.
    /// </summary>
    public interface IClientSignal;
}
