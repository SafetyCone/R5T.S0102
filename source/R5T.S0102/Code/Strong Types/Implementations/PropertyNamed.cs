using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IPropertyNamed"/>
    [StrongTypeImplementationMarker]
    public class PropertyNamed : TypedBase<string>, IStrongTypeMarker,
        IPropertyNamed
    {
        public PropertyNamed(string value)
            : base(value)
        {
        }
    }
}