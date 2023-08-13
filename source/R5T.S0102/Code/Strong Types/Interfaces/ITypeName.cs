using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as a type name.
    /// Note: this is type name of <strong>any</strong> type, whethe its a generic argument type argument (`0, or ``0), or namespaced type name (System.String).
    /// </summary>
    [StrongTypeMarker]
    public interface ITypeName : IStrongTypeMarker,
        ITypeNamed
    {
    }
}