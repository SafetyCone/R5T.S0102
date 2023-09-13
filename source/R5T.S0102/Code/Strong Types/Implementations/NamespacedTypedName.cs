using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="INamespacedTypedName"/>
    [StrongTypeImplementationMarker]
    public class NamespacedTypedName : TypedBase<string>, IStrongTypeMarker,
        INamespacedTypedName
    {
        public NamespacedTypedName(string value)
            : base(value)
        {
        }
    }
}