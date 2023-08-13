using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="INamespacedTypeNamed"/>
    [StrongTypeImplementationMarker]
    public class NamespacedTypeNamed : TypedBase<string>, IStrongTypeMarker,
        INamespacedTypeNamed
    {
        public NamespacedTypeNamed(string value)
            : base(value)
        {
        }
    }
}