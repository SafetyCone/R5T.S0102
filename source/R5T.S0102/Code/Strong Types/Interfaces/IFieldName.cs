using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as the name of a field.
    /// </summary>
    [StrongTypeMarker]
    public interface IFieldName : IStrongTypeMarker,
        IFieldNamed
    {
    }
}