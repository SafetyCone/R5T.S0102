using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as a being an identity name that is prefixed with the member kind marker (like "T:" for types, "M:" for methods).
    /// </summary>
    [StrongTypeMarker]
    public interface IKindMarked : IStrongTypeMarker,
        ITyped<string>
    {
    }
}