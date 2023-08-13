using System;

using R5T.T0178;
using R5T.T0179;


namespace R5T.S0102.N002
{
    /// <inheritdoc cref="IEventName"/>
    [StrongTypeImplementationMarker]
    public class EventName : TypedBase<string>, IStrongTypeMarker,
        IEventName
    {
        public EventName(string value)
            : base(value)
        {
        }
    }
}