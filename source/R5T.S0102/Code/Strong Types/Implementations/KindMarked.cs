using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IKindMarked"/>
    [StrongTypeImplementationMarker]
    public class KindMarked : TypedBase<string>, IStrongTypeMarker,
        IKindMarked
    {
        public KindMarked(string value)
            : base(value)
        {
        }
    }
}