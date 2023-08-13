using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as a namespace name.
    /// Note: for nested types, the namespace name includes the parent type name.
    /// </summary>
    [StrongTypeMarker]
    public interface INamespaceName : IStrongTypeMarker,
        // Yes, namespace names are namespaced.
        INamespacedName
    {
    }
}