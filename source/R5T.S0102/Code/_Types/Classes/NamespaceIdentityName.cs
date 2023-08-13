using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public class NamespaceIdentityName : IdentityName,
        IWithNamespaceName
    {
        public INamespaceName NamespaceName { get; set; }


        public NamespaceIdentityName()
        {
            this.KindMarker = Instances.KindMarkers.Namespace;
        }
    }
}
