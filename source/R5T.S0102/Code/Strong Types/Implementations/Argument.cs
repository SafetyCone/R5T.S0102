using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IArgument"/>
    [StrongTypeImplementationMarker]
    public class Argument : TypedBase<string>, IStrongTypeMarker,
        IArgument
    {
        public Argument(string value)
            : base(value)
        {
        }
    }
}