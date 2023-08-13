using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IArgumentList"/>
    [StrongTypeImplementationMarker]
    public class ArgumentList : TypedBase<string>, IStrongTypeMarker,
        IArgumentList
    {
        public ArgumentList(string value)
            : base(value)
        {
        }
    }
}