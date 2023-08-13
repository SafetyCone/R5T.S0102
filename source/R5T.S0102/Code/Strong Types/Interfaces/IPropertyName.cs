using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as a property name.
    /// </summary>
    [StrongTypeMarker]
    public interface IPropertyName : IStrongTypeMarker,
        IPropertyNamed
    {
    }
}