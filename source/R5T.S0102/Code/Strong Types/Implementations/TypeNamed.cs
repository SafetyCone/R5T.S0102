using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="ITypeNamed"/>
    [StrongTypeImplementationMarker]
    public class TypeNamed : TypedBase<string>, IStrongTypeMarker,
        ITypeNamed
    {
        public TypeNamed(string value)
            : base(value)
        {
        }
    }
}