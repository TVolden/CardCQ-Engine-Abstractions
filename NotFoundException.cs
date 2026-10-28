namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Something a command or query names doesn't exist, or not for the one asking. Packages derive their own exceptions from it,
    /// so a game host can answer "not found" without knowing the package.
    /// </summary>
    public abstract class NotFoundException(string message) : Exception(message);
}
