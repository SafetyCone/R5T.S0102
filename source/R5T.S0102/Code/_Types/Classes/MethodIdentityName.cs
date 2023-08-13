using System;

using R5T.S0102.N002;


namespace R5T.S0102.N001
{
    public class MethodIdentityName : IdentityName,
        IWithArgumentTypeNames,
        IWithMethodName,
        IWithOutputTypeName,
        IWithTypeIdentityName,
        IWithTypeParameterCount
    {
        public TypeIdentityName TypeIdentityName { get; set; }
        public IMethodName MethodName { get; set; }
        public int TypeParameterCount { get; set; }
        public ITypeName[] ArgumentTypeNames { get; set; }
        /// <summary>
        /// Only available for conversion operator methods.
        /// </summary>
        public ITypeName OutputTypeName { get; set; }


        public MethodIdentityName()
        {
            this.KindMarker = Instances.KindMarkers.Method;
        }
    }
}
