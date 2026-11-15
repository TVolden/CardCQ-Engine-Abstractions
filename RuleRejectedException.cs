namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// The rules rejected what a player tried, for example an answer a prompt doesn't take, or an action out of turn. The
    /// message is for the player, so a game host tells them why. Packages throw it, or derive their own exceptions from it.
    /// Anything else a command throws while the rules run is taken as a mistake in the rules or a package, for the game's
    /// designer to fix.
    /// </summary>
    public class RuleRejectedException(string message) : Exception(message);
}
