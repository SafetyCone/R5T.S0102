using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as name with a namespaced, typed, parameter typed, name (basically, a method name, though also an indexer property name).
    /// </summary>
    [StrongTypeMarker]
    public interface INamespacedTypedParameterTypedName : IStrongTypeMarker,
        INamespacedTypeNamed
    {
    }
}