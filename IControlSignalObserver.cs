namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Custom observer of control signals, used to initiate behavior in response to a raised signal.
    /// </summary>
    public interface IControlSignalObserver<T> where T : IControlSignal
    {
        /// <summary>
        /// Triggered by the system when a specific control signal is raised.
        /// </summary>
        /// <param name="signal">The specific control signal</param>
        Task SignalRaised(T signal);
    }
}
