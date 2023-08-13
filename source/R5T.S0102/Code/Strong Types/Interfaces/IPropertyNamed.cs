using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as containing a property name.
    /// </summary>
    [StrongTypeMarker]
    public interface IPropertyNamed : IStrongTypeMarker,
        ITyped<string>
    {
    }
}