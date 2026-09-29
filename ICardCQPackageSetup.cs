using System.Reflection;

namespace CardCQ.Engine.Abstractions
{
    public interface ICardCQPackageSetup
    {
        Task Setup(ICardCQService service);
        static abstract Assembly GetAssembly { get; }
        static abstract string Color { get; }
    }
}
