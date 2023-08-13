using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IIdentityNameValued"/>
    [StrongTypeImplementationMarker]
    public class IdentityNameValued : TypedBase<string>, IStrongTypeMarker,
        IIdentityNameValued
    {
        public IdentityNameValued(string value)
            : base(value)
        {
        }
    }
}