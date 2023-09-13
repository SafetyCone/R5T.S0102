using System;
using System.Linq;
using System.Reflection;

using R5T.T0132;

using R5T.S0102.N002;
using R5T.S0102.N002.Extensions;


namespace R5T.S0102
{
    [FunctionalityMarker]
    public partial interface IMemberInfoOperator : IFunctionalityMarker
    {
        public IIdentityName Get_IdentityName(MemberInfo memberInfo)
        {
            var output = memberInfo switch
            {
                MethodInfo methodInfo => this.Get_IdentityName(methodInfo),
                EventInfo eventInfo => this.Get_IdentityName(eventInfo),
                FieldInfo fieldInfo => this.Get_IdentityName(fieldInfo),
                PropertyInfo propertyInfo => this.Get_IdentityName(propertyInfo),
                TypeInfo typeInfo => this.Get_IdentityName(typeInfo),
                ConstructorInfo constructorInfo => this.Get_ConstructorIdentityName(constructorInfo),
                _ => throw new Exception($"Unrecognzed member info type: {memberInfo}"),
            }; ;

            return output;
        }

        public IIdentityName Get_ConstructorIdentityName(ConstructorInfo constructorInfo)
        {
            var namespacedTypeName = Instances.TypeOperator.Get_NamespacedTypeName(constructorInfo.DeclaringType);

            var name = "#ctor";

            var namespacedTypedName = Instances.IdentityNameOperator_Internal.Combine(
                namespacedTypeName,
                name);

            var parametersPart = this.Get_ParametersPart(constructorInfo.GetParameters());

            var namespacedTypedParameterTypedName = Instances.IdentityNameOperator_Internal.Append(
                namespacedTypedName,
                parametersPart)
                .ToNamespacedTypedParameterTypedName();

            var output = Instances.IdentityNameOperator_N002.ToMethodIdentityname(namespacedTypedParameterTypedName);
            return output;
        }

        public IIdentityName Get_IdentityName(EventInfo eventInfo)
        {
            var namespacedTypedName = this.Get_NamespacedTypedName(eventInfo);

            var output = Instances.IdentityNameOperator_N002.ToEventIdentityname(namespacedTypedName);
            return output;
        }

        public IIdentityName Get_IdentityName(FieldInfo fieldInfo)
        {
            var namespacedTypedName = this.Get_NamespacedTypedName(fieldInfo);

            var output = Instances.IdentityNameOperator_N002.ToFieldIdentityname(namespacedTypedName);
            return output;
        }

        public IIdentityName Get_IdentityName(MethodInfo methodInfo)
        {
            var namespacedTypedName = this.Get_NamespacedTypedName(methodInfo);

            var methodName = namespacedTypedName.Value;

            var isGeneric = Instances.MethodInfoOperator.Is_Generic(methodInfo);
            if(isGeneric)
            {
                methodName = Instances.IdentityNameOperator_Internal.Append_MethodTypeParametersCount(
                    methodName,
                    methodInfo);
            }

            try
            {
                // Need to account for parameter types.
                var parameterInfos = methodInfo.GetParameters();

                var parametersPart = this.Get_ParametersPart(parameterInfos);

                methodName = Instances.IdentityNameOperator_Internal.Append(
                    methodName,
                    parametersPart);
            }
            // Function pointers are not supported.
            catch (NotSupportedException)
            {
                // Do nothing.
            }

            // Handle implicit and explicit conversion operators.
            if(methodInfo.Name == "op_Explicit" || methodInfo.Name == "op_Implicit")
            {
                // Append the return type.
                var returnType = methodInfo.ReturnType;

                // Use the parameter type code for the return type.
                var returnTypeName = Instances.TypeOperator.Get_NamespacedTypeName_ForParameterType(returnType);

                // Weirdness for conversion operators and 
                if (returnType.IsGenericTypeParameter)
                {
                    var positionBasedTypeName = "`" + returnType.GenericParameterPosition;
                    var genericTypeName = returnType.Name;

                    returnTypeName = returnTypeName.Replace(positionBasedTypeName, genericTypeName);
                }

                methodName += Instances.TokenSeparators.OutputTypeTokenSeparator + returnTypeName;
            }

            var namespacedTypedParameterTypedName = methodName
                .ToNamespacedTypedParameterTypedName();

            var output = Instances.IdentityNameOperator_N002.ToMethodIdentityname(namespacedTypedParameterTypedName);
            return output;
        }

        public string Get_ParametersPart(ParameterInfo[] parameters)
        {
            if(!parameters.Any())
            {
                // No parameters means no parentheses.
                return String.Empty;
            }

            var parameterTypes = Instances.StringOperator.Join(
                Instances.TokenSeparators.ArgumentListSeparator,
                parameters
                    .Select(parameter => Instances.ParameterInfoOperator.Get_NamespacedTypeName_ForParameter(parameter)
                ));

            var output = $"{Instances.TokenSeparators.ParameterListOpenTokenSeparator}{parameterTypes}{Instances.TokenSeparators.ParameterListCloseTokenSeparator}";
            return output;
        }

        public IIdentityName Get_IdentityName(PropertyInfo propertyInfo)
        {
            var namespacedTypedName = this.Get_NamespacedTypedName(propertyInfo);

            var propertyName = namespacedTypedName.Value;

            var isIndexer = Instances.PropertyInfoOperator.Is_Indexer(propertyInfo);
            if(isIndexer)
            {
                var parameterInfos = Instances.PropertyInfoOperator.Get_IndexerParameters(propertyInfo);

                var parametersPart = this.Get_ParametersPart(parameterInfos);

                propertyName = Instances.IdentityNameOperator_Internal.Append(
                    propertyName,
                    parametersPart);
            }

            var finalNamespacedTypedName = propertyName
                .ToNamespacedTypedName();

            var output = Instances.IdentityNameOperator_N002.ToPropertyIdentityname(finalNamespacedTypedName);
            return output;
        }

        public IIdentityName Get_IdentityName(TypeInfo typeInfo)
        {
            var namespacedTypeName = Instances.TypeOperator.Get_NamespacedTypeName(typeInfo)
                .ToNamespacedTypeName();

            var output = Instances.IdentityNameOperator_N002.ToTypeIdentityName(namespacedTypeName);
            return output;
        }

        public INamespacedTypedName Get_NamespacedTypedName(MemberInfo memberInfo)
        {
            var namespacedTypeName = Instances.TypeOperator.Get_NamespacedTypeName(memberInfo.DeclaringType);
            var memberName = this.Get_Name_Adjusted(memberInfo);

            var output = Instances.IdentityNameOperator_Internal.Combine(namespacedTypeName, memberName)
                .ToNamespacedTypedName();

            return output;
        }

        /// <summary>
        /// Gets the raw name of a member (<see cref="MemberInfo.Name"/>),
        /// without any adjustements.
        /// </summary>
        public string Get_Name(MemberInfo memberInfo)
        {
            var output = memberInfo.Name;
            return output;
        }

        /// <summary>
        /// Gets the name of a member, adjusted for:
        /// 1) Whether the member is explicitly implemented,
        /// 2) Whether the member name contains generic type arguments (for example, when explicitly implementing a generic interface),
        /// 3) Whether the member name contains multiple generic type arguments (for example, when explicitly implementing a generic interface)
        /// .
        /// </summary>
        public string Get_Name_Adjusted(MemberInfo memberInfo)
        {
            var output = this.Get_Name(memberInfo);

            // If the name contains dots ('.'), then it's an explicit implementation.
            var isExplicitlyImplemented = this.Is_ExplicitlyImplemented(memberInfo);
            if(isExplicitlyImplemented)
            {
                //// Tuple-craziness!
                //// In: C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\6.0.21\ref\net6.0\System.Collections.dll
                //// Desired: M:System.Collections.Generic.PriorityQueue`2.UnorderedItemsCollection.System#Collections#Generic#IEnumerable{System#ValueTuple{TElement@TPriority}}#GetEnumerator
                //// Result: M:System.Collections.Generic.PriorityQueue`2.UnorderedItemsCollection.System#Collections#Generic#IEnumerable{(TElementElement@TPriorityPriority)}#GetEnumerator
                //var containsTuple = output.Contains('(');
                //if(containsTuple)
                //{
                //    var declaringType = memberInfo.DeclaringType;

                //    var declaringTypeInterfaceTypes = declaringType.GetInterfaces();
                //    foreach (var interfaceType in declaringTypeInterfaceTypes)
                //    {
                //        // Fails with exception: System.NotSupportedException: 'InterfaceMapping is not supported on assemblies loaded by a MetadataLoadContext.'
                //        var interfaceMap = declaringType.GetInterfaceMap(interfaceType);

                //        var interfaceContainsMember = interfaceMap.TargetMethods.Contains(memberInfo);
                //        if(interfaceContainsMember)
                //        {
                //            var containingInterfaceType = interfaceMap.InterfaceType;

                //            output = containingInterfaceType.Name;

                //            break;
                //        }
                //    }
                //}

                // Convert all namespace token separators ('.', periods) to hashes ('#').
                output = Instances.StringOperator.Replace(
                    output,
                    Instances.TokenSeparators.ExplicitImplementationNamespaceTokenSeparator,
                    Instances.TokenSeparators.NamespaceTokenSeparator);

                // Convert all generic type list open and close token separators ('<' and '>', angle-braces) to braces ('{' and '}' respectively).
                output = Instances.StringOperator.Replace(
                    output,
                    Instances.TokenSeparators.TypeArgumentListOpenSeparator,
                    '<');

                output = Instances.StringOperator.Replace(
                    output,
                    Instances.TokenSeparators.TypeArgumentListCloseSeparator,
                    '>');

                // Convert all generic type list element separators (',', comma) to at-signs ('@', at-sign, alphasand).
                output = Instances.StringOperator.Replace(
                    output,
                    '@',
                    Instances.TokenSeparators.ArgumentListSeparator);
            }

            return output;
        }

        /// <summary>
        /// Member names that are explicitly implemented are the full namespaced typed name of the implemented member.
        /// Thus, they contain the namespaced token separator.
        /// </summary>
        public bool Is_ExplicitlyImplemented(MemberInfo memberInfo)
        {
            var rawName = this.Get_Name(memberInfo);

            var output = Instances.StringOperator.Contains(
                rawName,
                Instances.TokenSeparators.NamespaceTokenSeparator);

            return output;
        }
    }
}
