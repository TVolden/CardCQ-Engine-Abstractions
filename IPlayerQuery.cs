namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a query whose answer depends on who is looking: it shows <see cref="Player"/> only what that player may see.
    /// A game host answering it for a client always sets <see cref="Player"/> to the client's own player, never to what the
    /// client asked for.
    /// </summary>
    public interface IPlayerQuery : IClientQuery
    {
        /// <summary>The player the answer is for.</summary>
        int Player { get; }
    }
}
