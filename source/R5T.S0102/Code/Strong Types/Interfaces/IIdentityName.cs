using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as a being an identity name, the type of which can participate in decomposition logic based on other strong types in this namespace.
    /// </summary>
    [StrongTypeMarker]
    public interface IIdentityName : IStrongTypeMarker,
        // All identity names are kind-marked.
        IKindMarked,
        // All identity names have a value, even if it's empty.
        IIdentityNameValued
    {
    }
}
