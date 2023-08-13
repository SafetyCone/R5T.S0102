using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public class EventIdentityName : IdentityName,
        IWithTypeIdentityName,
        IWithEventName
    {
        public TypeIdentityName TypeIdentityName { get; set; }
        public IEventName EventName { get; set; }


        public EventIdentityName()
        {
            this.KindMarker = Instances.KindMarkers.Event;
        }
    }
}
