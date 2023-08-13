using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IErrorMessaged"/>
    [StrongTypeImplementationMarker]
    public class ErrorMessaged : TypedBase<string>, IStrongTypeMarker,
        IErrorMessaged
    {
        public ErrorMessaged(string value)
            : base(value)
        {
        }
    }
}