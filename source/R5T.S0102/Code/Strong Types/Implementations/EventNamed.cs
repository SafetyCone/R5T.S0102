using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IEventNamed"/>
    [StrongTypeImplementationMarker]
    public class EventNamed : TypedBase<string>, IStrongTypeMarker,
        IEventNamed
    {
        public EventNamed(string value)
            : base(value)
        {
        }
    }
}