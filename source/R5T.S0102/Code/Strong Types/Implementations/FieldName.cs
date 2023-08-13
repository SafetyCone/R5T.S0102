using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IFieldName"/>
    [StrongTypeImplementationMarker]
    public class FieldName : TypedBase<string>, IStrongTypeMarker,
        IFieldName
    {
        public FieldName(string value)
            : base(value)
        {
        }
    }
}