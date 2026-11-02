namespace CardCQ.Engine.Abstractions
{
    /// <summary>
    /// Marks a concept that only packages dispatch, e.g. a query a command uses internally. The engine doesn't offer it to
    /// scripts by name, and a platform doesn't list it as a code block. Packages use it by its type, like any other concept.
    /// </summary>
    public interface IPackageOnly;
}
