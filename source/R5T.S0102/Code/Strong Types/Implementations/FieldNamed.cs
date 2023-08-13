using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IFieldNamed"/>
    [StrongTypeImplementationMarker]
    public class FieldNamed : TypedBase<string>, IStrongTypeMarker,
        IFieldNamed
    {
        public FieldNamed(string value)
            : base(value)
        {
        }
    }
}