using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IErrorMessage"/>
    [StrongTypeImplementationMarker]
    public class ErrorMessage : TypedBase<string>, IStrongTypeMarker,
        IErrorMessage
    {
        public ErrorMessage(string value)
            : base(value)
        {
        }
    }
}