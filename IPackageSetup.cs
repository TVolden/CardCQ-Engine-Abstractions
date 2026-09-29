using System.Reflection;

namespace CardCQ.Engine.Abstractions
{
    public interface IPackageSetup
    {
        Task Setup(ICQService service);
        static abstract Assembly GetAssembly { get; }
        static abstract string Color { get; }
    }
}
