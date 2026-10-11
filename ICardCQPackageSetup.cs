using System.Reflection;

namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Defines a package setup that can be used to register handlers, decorators, and observers for the CardCQ engine.
    /// A package is required to have exactly one implementation of this interface, which will be called by the engine to set up the package.
    /// </summary>
    public interface ICardCQPackageSetup
    {
        /// <summary>
        /// Setup the package by instantiating and registering handlers, decorators, and observers with the provided <see cref="ICardCQService"/>.
        /// </summary>
        /// <param name="service">The service to register handlers, decorators, and observers with</param>
        Task Setup(ICardCQService service);

        /// <summary>
        /// Gets the assembly that contains the package setup implementation. This is used to locate the package setup implementation when loading packages.
        /// </summary>
        static abstract Assembly GetAssembly { get; }
    }
}
