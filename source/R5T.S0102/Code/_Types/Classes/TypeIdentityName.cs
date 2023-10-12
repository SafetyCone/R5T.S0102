using System;

using R5T.T0142;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    [DataTypeMarker]
    public class TypeIdentityName : IdentityName,
        IWithNamespaceName,
        IWithTypeName,
        IWithTypeParameterCount
    {
        public INamespaceName NamespaceName { get; set; }
        public ITypeName TypeName { get; set; }
        public int TypeParameterCount { get; set; }


        public TypeIdentityName()
        {
            KindMarker = Instances.KindMarkers.Type;
        }
    }
}
