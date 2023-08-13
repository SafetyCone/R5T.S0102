using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IIdentityName"/>
    [StrongTypeImplementationMarker]
    public class IdentityName : TypedBase<string>, IStrongTypeMarker,
        IIdentityName
    {
        public IdentityName(string value)
            : base(value)
        {
        }
    }
}