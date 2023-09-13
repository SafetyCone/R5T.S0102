using System;
using System.Reflection;

using R5T.T0132;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IParameterInfoOperator : IFunctionalityMarker,
        L0053.IParameterInfoOperator
    {
        public string Get_NamespacedTypeName_ForParameter(ParameterInfo parameterInfo)
        {
            var typeNamespacedTypeName = Instances.TypeOperator.Get_NamespacedTypeName_ForParameterType(parameterInfo.ParameterType);

            var output = typeNamespacedTypeName;

            // Adjust for mismatch in by-reference marker character between MemberInfo.Name ('&') and the identity name ('@')s.
            var isByRef = this.Is_Reference(parameterInfo);
            if(isByRef)
            {
                output = output[..^1] + Instances.TokenSeparators.ByReferenceMarker;
            }

            // Special behavior for implicit and explicit conversion operators on generic types.
            if (parameterInfo.Member is MethodInfo methodInfo)
            {
                if (methodInfo.Name == "op_Implicit" || methodInfo.Name == "op_Explicit")
                {
                    if(parameterInfo.ParameterType.IsGenericTypeParameter)
                    {
                        var positionBasedTypeName = "`" + parameterInfo.ParameterType.GenericParameterPosition;
                        var genericTypeName = parameterInfo.ParameterType.Name;

                        output = output.Replace(positionBasedTypeName, genericTypeName);
                    }

                    if (parameterInfo.ParameterType.HasElementType)
                    {
                        var elementType = parameterInfo.ParameterType.GetElementType();

                        if (elementType.IsGenericTypeParameter)
                        {
                            var positionBasedTypeName = "`" + elementType.GenericParameterPosition;
                            var genericTypeName = elementType.Name;

                            output = output.Replace(positionBasedTypeName, genericTypeName);
                        }
                    }
                }
            }

            return output;
        }
    }
}
