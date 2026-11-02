namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Pauses the game while a player makes a choice, e.g. picks a card. The handler that waits is paused, and with it the
    /// command that dispatched it; nothing else runs until the wait ends, though a game host still answers the clients'
    /// queries. A wait ends when it is answered, when its time runs out, or when it is withdrawn. There is one wait at a time.
    /// </summary>
    public interface IPlayerWaits
    {
        /// <summary>The wait the game is paused on, or null when it isn't paused.</summary>
        PlayerWait? Waiting { get; }

        /// <summary>
        /// Pauses the game until <paramref name="player"/> answers, at most for <paramref name="timeout"/>. Answers the value given to
        /// <see cref="AnswerAsync"/>, or null when the time ran out or the wait was withdrawn. The package that waits checks and
        /// reads the answer, so any kind of answer can be waited for.
        /// </summary>
        /// <remarks>
        /// Not a pure query: it opens the wait. Waiting is how a query asks a player (e.g. a card to pick), so it changes the
        /// game's course while it answers.
        /// </remarks>
        /// <exception cref="InvalidOperationException">The game is already waiting.</exception>
        Task<object?> WaitForPlayerAsync(int player, TimeSpan timeout, CancellationToken ct);

        /// <summary>
        /// Ends the wait with <paramref name="answer"/>. The paused command goes on, and this returns once it finished or paused again.
        /// </summary>
        /// <exception cref="InvalidOperationException">The game isn't waiting.</exception>
        Task AnswerAsync(object answer);

        /// <summary>Ends the wait without an answer, if the game is waiting. Returns once the paused command finished or paused again.</summary>
        Task WithdrawAsync();
    }
}
