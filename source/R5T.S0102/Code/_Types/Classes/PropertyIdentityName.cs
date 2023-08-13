using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public class PropertyIdentityName : IdentityName,
        IWithArgumentTypeNames,
        IWithPropertyName,
        IWithTypeIdentityName
    {
        public TypeIdentityName TypeIdentityName { get; set; }
        public IPropertyName PropertyName { get; set; }
        public ITypeName[] ArgumentTypeNames { get; set; }


        public PropertyIdentityName()
        {
            this.KindMarker = Instances.KindMarkers.Property;
        }
    }
}
