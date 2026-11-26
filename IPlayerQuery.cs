namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a query whose answer depends on who is looking: it shows <see cref="Player"/> only what that player may see.
    /// A game host answering it for a client always sets <see cref="Player"/> to the client's own player, never to what the
    /// client asked for. For a spectator (someone watching without a seat) it sets <see cref="Player"/> to 0: answer with
    /// what every player and an outsider may see, and offer nothing to act on.
    /// </summary>
    public interface IPlayerQuery : IClientQuery
    {
        /// <summary>The player the answer is for: 1 and up for a seat, 0 for a spectator.</summary>
        int Player { get; }
    }
}
