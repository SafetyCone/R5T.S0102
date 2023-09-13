using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="INamespacedTypedParameterTypedName"/>
    [StrongTypeImplementationMarker]
    public class NamespacedTypedParameterTypedName : TypedBase<string>, IStrongTypeMarker,
        INamespacedTypedParameterTypedName
    {
        public NamespacedTypedParameterTypedName(string value)
            : base(value)
        {
        }
    }
}