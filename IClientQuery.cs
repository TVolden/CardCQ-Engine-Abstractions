namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a query whose answer reveals nothing hidden from the players, so a game host may answer it for any player's
    /// client. Queries without the marker are for the game's rules only, which see everything.
    /// The engine doesn't check the marker; a host decides by it what a client may ask.
    /// </summary>
    public interface IClientQuery;
}
