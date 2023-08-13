using System;

using R5T.T0178;


namespace R5T.S0102.N002
{
    /// <summary>
    /// Strongly-types a string as an identity name error message.
    /// </summary>
    [StrongTypeMarker]
    public interface IErrorMessage : IStrongTypeMarker,
        IErrorMessaged
    {
    }
}