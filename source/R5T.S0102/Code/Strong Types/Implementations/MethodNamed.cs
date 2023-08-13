using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IMethodNamed"/>
    [StrongTypeImplementationMarker]
    public class MethodNamed : TypedBase<string>, IStrongTypeMarker,
        IMethodNamed
    {
        public MethodNamed(string value)
            : base(value)
        {
        }
    }
}