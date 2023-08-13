using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="INamespacedName"/>
    [StrongTypeImplementationMarker]
    public class NamespacedName : TypedBase<string>, IStrongTypeMarker,
        INamespacedName
    {
        public NamespacedName(string value)
            : base(value)
        {
        }
    }
}