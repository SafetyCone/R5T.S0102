using System;
using System.Text;

using R5T.L0053.Extensions;
using R5T.T0132;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface ITypeOperator : IFunctionalityMarker,
        L0053.ITypeOperator
    {
        /// <summary>
        /// For a type instance that specifies the type of a parameter,
        /// get the namespaced type name, inclusive of any generic type parameters.
        /// </summary>
        public string Get_NamespacedTypeName_ForParameterType(Type type)
        {
            // Does the type have an element type? (Arrays, by-reference, etc.)
            var hasElementType = type.HasElementType;
            if(hasElementType)
            {
                // If the type has an element type, get the element type name, then figure out the array suffix and add it.
                var elementType = type.GetElementType();

                var elementTypeNamespacedTypeName = this.Get_NamespacedTypeName_ForParameterType(elementType);

                // Is the type an array type?
                var isArray = type.IsArray;
                if(isArray)
                {
                    var output = elementTypeNamespacedTypeName + Instances.TypeNameAffixes.ForArray_Suffix;
                    return output;
                }

                var isByRef = type.IsByRef;
                if(isByRef)
                {
                    var output = elementTypeNamespacedTypeName + Instances.TypeNameAffixes.ForByReference_Suffix;
                    return output;
                }

                var isByPointer = type.IsPointer;
                if(isByPointer)
                {
                    var output = elementTypeNamespacedTypeName + Instances.TypeNameAffixes.ForPointer_Suffix;
                    return output;
                }

                throw new Exception("Unknown element type relationship.");
            }

            // Is the type a generic parameter type?
            var isGenericParameterType = type.IsGenericParameter;
            if(isGenericParameterType)
            {
                // Return the position, with some prefix.
                var position = type.GenericParameterPosition;

                var isGenericTypeParameterType = type.IsGenericTypeParameter;
                if(isGenericTypeParameterType)
                {
                    var output = $"{Instances.TypeNameAffixes.ForGenericTypeParameterType_Prefix}{position}";
                    return output;
                }

                var isGenericMethodParameterType = type.IsGenericMethodParameter;
                if(isGenericMethodParameterType)
                {
                    var output = $"{Instances.TypeNameAffixes.ForGenericMethodParameterType_Prefix}{position}";
                    return output;
                }

                throw new Exception("Unknown generic parameter type relationship.");
            }

            // At this point, we have an actual type.s
            var namespacedTypeName = this.Get_NamespacedTypeName(type);

            // Is the type a constructed generic type?
            var isConstructedGenericType = type.IsConstructedGenericType;
            if(isConstructedGenericType)
            {
                var builder = new StringBuilder();

                var beginningOfNamespacedTypeName = Instances.NamespacedTypeNameOperator.Get_Substring_Upto_GenericTypeParameterCount(namespacedTypeName);
                var endingOfNamespacedTypeName = Instances.NamespacedTypeNameOperator.Get_Substring_After_GenericTypeParameterCount(namespacedTypeName);

                builder.Append(beginningOfNamespacedTypeName);

                var typeArguments = this.Get_GenericTypeArguments(type);

                builder.Append(Instances.TokenSeparators.TypeArgumentListOpenSeparator);

                foreach (var typeArgument in typeArguments)
                {
                    var typeName = this.Get_NamespacedTypeName_ForParameterType(typeArgument);

                    builder.Append(typeName);
                    builder.Append(Instances.TokenSeparators.ArgumentListSeparator);
                }

                builder.Remove_Last();

                builder.Append(Instances.TokenSeparators.TypeArgumentListCloseSeparator);

                builder.Append(endingOfNamespacedTypeName);

                var output = builder.ToString();
                return output;
            }

            // Else, just return the namespaced type name.
            return namespacedTypeName;
        }
    }
}
