namespace CardCQ.Engine.Abstractions
{
    /// <summary>The game is paused until <see cref="Player"/> answers, at the latest until <see cref="Deadline"/>.</summary>
    public sealed record PlayerWait(int Player, DateTimeOffset Deadline);
}
