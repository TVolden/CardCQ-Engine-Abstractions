namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Custom observer of game signals, used to initiate behavior in response to a raised signal.
    /// </summary>
    public interface IGameSignalObserver<T> where T : IGameSignal
    {
        /// <summary>
        /// Triggered by the system when a specific game signal is raised.
        /// </summary>
        /// <param name="signal">The specific game signal</param>
        Task SignalRaised(T signal);
    }
}
