using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public class FieldIdentityName : IdentityName,
        IWithTypeIdentityName,
        IWithFieldName
    {
        public TypeIdentityName TypeIdentityName { get; set; }
        public IFieldName FieldName { get; set; }


        public FieldIdentityName()
        {
            this.KindMarker = Instances.KindMarkers.Field;
        }
    }
}
