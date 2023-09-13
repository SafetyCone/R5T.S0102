using System;
using System.Linq;

using R5T.T0132;
using R5T.T0162;

using R5T.S0102.N002.Extensions;

using IInternalIdentityName = R5T.S0102.N002.IIdentityName;


namespace R5T.S0102.N001
{
    public partial interface IIdentityNameOperator : IFunctionalityMarker
    {
        public PropertyIdentityName Get_PropertyIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Property);

            var identityNameValue = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value);

            // The namespaced, typed, method name is the identity name value up to the opening parenthesis of the arguments list (if it exists).
            var indexOfOpenParenthesisFound = Instances.StringOperator.IndexOf(
                Instances.TokenSeparators.ParameterListOpenTokenSeparator,
                identityNameValue);

            var namespacedTypedMethodName = indexOfOpenParenthesisFound
                // If there is a parenthesis, then get up to the parenthesis.
                ? Instances.StringOperator.Get_Substring_Upto_Exclusive(
                    indexOfOpenParenthesisFound,
                    identityNameValue)
                // Else, use the whole identity name value.
                : identityNameValue
                ;

            // Treat the namespaced type name as the namespace.
            var namespacedTypeName = Instances.NamespacedTypeNameOperator.Get_NamespaceName(namespacedTypedMethodName);

            // Treat the property name as the type name.
            var propertyName = Instances.NamespacedTypeNameOperator.Get_TypeName(namespacedTypedMethodName);

            var typeIdentityName = this.Get_TypeIdentityName(namespacedTypeName);

            var argumentListValue = Internal.Get_ArgumentListValue(identityNameValue);

            var argumentValues = Internal.Get_Arguments(argumentListValue);

            var argumentTypeNames = argumentValues
                .Select(x => x.ToTypeName())
                .ToArray();

            var output = new PropertyIdentityName()
            {
                ArgumentTypeNames = argumentTypeNames,
                PropertyName = propertyName.ToPropertyName(),
                TypeIdentityName = typeIdentityName,
            };

            return output;
        }

        public IInternalIdentityName Get_PropertyIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<PropertyIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_PropertyIdentityName);

            return output;
        }

        public IInternalIdentityName Get_PropertyIdentityName(PropertyIdentityName propertyIdentityName)
        {
            var namespacedTypedMethodName = this.Get_FieldIdentityName(
                propertyIdentityName.TypeIdentityName,
                propertyIdentityName.PropertyName.Value,
                propertyIdentityName.KindMarker);

            var argumentList = Instances.StringOperator.Join(
                Instances.TokenSeparators.ArgumentListSeparator,
                propertyIdentityName.ArgumentTypeNames
                    .Select(x => x.Value));

            var parameters = propertyIdentityName.ArgumentTypeNames.Any()
                ? $"{Instances.TokenSeparators.ParameterListOpenTokenSeparator}{argumentList}{Instances.TokenSeparators.ParameterListCloseTokenSeparator}"
                : String.Empty
                ;

            var output = $"{namespacedTypedMethodName}{parameters}"
                .ToIdentityName();

            return output;
        }

        public MethodIdentityName Get_MethodIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Method);

            var identityNameValue = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value);

            // The namespaced, typed, method name is the identity name value up to the opening parenthesis of the arguments list (if it exists).
            var indexOfOpenParenthesisFound = Instances.StringOperator.IndexOf(
                Instances.TokenSeparators.ParameterListOpenTokenSeparator,
                identityNameValue);

            var namespacedTypedMethodName = indexOfOpenParenthesisFound
                // If there is a parenthesis, then get up to the parenthesis.
                ? Instances.StringOperator.Get_Substring_Upto_Exclusive(
                    indexOfOpenParenthesisFound,
                    identityNameValue)
                // Else, use the whole identity name value.
                : identityNameValue
                ;

            // Treat the namespaced type name as the namespace.
            var namespacedTypeName = Instances.NamespacedTypeNameOperator.Get_NamespaceName(namespacedTypedMethodName);

            // Treat the method name as the type name.
            var methodName = Instances.NamespacedTypeNameOperator.Get_TypeName(namespacedTypedMethodName);

            var isGeneric = Internal.Is_GenericMethodName(methodName);

            var typeParameterCountToken = isGeneric
                ? Instances.StringOperator.Get_Substring_From_Exclusive(
                    Instances.TokenSeparators.MethodTypeParameterCountSeparator,
                    methodName)
                : "0"
                ;

            var typeParameterCount = Instances.IntegerOperator.Parse(typeParameterCountToken);

            var typeIdentityName = this.Get_TypeIdentityName(namespacedTypeName);

            var argumentListValue = Internal.Get_ArgumentListValue(identityNameValue);

            var argumentValues = Internal.Get_Arguments(argumentListValue);

            var argumentTypeNames = argumentValues
                .Select(x => x.ToTypeName())
                .ToArray();

            var hasOutputType = Instances.IdentityNameOperator_Internal.Has_OutputType(identityNameValue);

            var outputTypeName = hasOutputType
                ? hasOutputType.Result.ToTypeName()
                : null
                ;

            var output = new MethodIdentityName()
            {
                ArgumentTypeNames = argumentTypeNames,
                MethodName = methodName.ToMethodName(),
                OutputTypeName = outputTypeName,
                TypeIdentityName = typeIdentityName,
                TypeParameterCount = typeParameterCount,
            };

            return output;
        }

        public IInternalIdentityName Get_MethodIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<MethodIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_MethodIdentityName);

            return output;
        }

        public IInternalIdentityName Get_MethodIdentityName(MethodIdentityName methodIdentityName)
        {
            var namespacedTypedMethodName = this.Get_FieldIdentityName(
                methodIdentityName.TypeIdentityName,
                methodIdentityName.MethodName.Value,
                methodIdentityName.KindMarker);

            var argumentList = Instances.StringOperator.Join(
                Instances.TokenSeparators.ArgumentListSeparator,
                methodIdentityName.ArgumentTypeNames
                    .Select(x => x.Value));

            var parameters = methodIdentityName.ArgumentTypeNames.Any()
                ? $"{Instances.TokenSeparators.ParameterListOpenTokenSeparator}{argumentList}{Instances.TokenSeparators.ParameterListCloseTokenSeparator}"
                : String.Empty
                ;

            var outputTypeToken = methodIdentityName.OutputTypeName is object
                ? $"{Instances.TokenSeparators.OutputTypeTokenSeparator}{methodIdentityName.OutputTypeName}"
                : String.Empty
                ;

            var output = $"{namespacedTypedMethodName}{parameters}{outputTypeToken}"
                .ToIdentityName();

            return output;
        }

        public Error Get_Error(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Error);

            // The value of the error identity name is the identity name message.
            var errorMessage = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value)
                .ToErrorMessage();

            var output = new Error()
            {
                ErrorMessage = errorMessage,
            };

            return output;
        }

        public IInternalIdentityName Get_Error(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<Error, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_Error);

            return output;
        }

        public IInternalIdentityName Get_Error(Error errorIdentityName)
        {
            var output = Base.Get_IdentityName(
                errorIdentityName.KindMarker.Value,
                // The error message is the value.
                errorIdentityName.ErrorMessage.Value)
                .ToIdentityName();

            return output;
        }

        public NamespaceIdentityName Get_NamespaceIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Namespace);

            // The value of the error identity name is the identity name message.
            var namespaceName = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value)
                .ToNamespaceName();

            var output = new NamespaceIdentityName()
            {
                NamespaceName = namespaceName,
            };

            return output;
        }

        public IInternalIdentityName Get_NamespaceIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<NamespaceIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_NamespaceIdentityName);

            return output;
        }

        public IInternalIdentityName Get_NamespaceIdentityName(NamespaceIdentityName errorIdentityName)
        {
            var output = Base.Get_IdentityName(
                errorIdentityName.KindMarker.Value,
                // The namespace name is the value.
                errorIdentityName.NamespaceName.Value)
                .ToIdentityName();

            return output;
        }

        public TypeIdentityName Get_TypeIdentityName(string typeIdentityNameValue)
        {
            // For types, the namespaced type name is the whole identity name (since nested types will include the parent type name in their namespace name).
            var namespacedTypeName = typeIdentityNameValue;

            var namespaceName = Instances.NamespacedTypeNameOperator.Get_NamespaceName(namespacedTypeName)
                .ToNamespaceName();

            var typeName = Instances.NamespacedTypeNameOperator.Get_TypeName(namespacedTypeName)
                .ToTypeName();

            // Is type name generic?
            var isGeneric = Internal.Is_GenericTypeName(typeName.Value);

            var typeParameterCountToken = isGeneric
                ? Instances.StringOperator.Get_Substring_From_First(
                    typeIdentityNameValue,
                    Instances.TokenSeparators.TypeParameterCountSeparator)
                // If not generic, then there are zero type parameters.
                : "0"
                ;

            var typeParameterCount = Instances.IntegerOperator.Parse(typeParameterCountToken);

            var output = new TypeIdentityName()
            {
                NamespaceName = namespaceName,
                TypeName = typeName,
                TypeParameterCount = typeParameterCount,
            };

            return output;
        }

        public TypeIdentityName Get_TypeIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Type);

            // The value of the error identity name is the identity name message.
            var typeIdentityNameValue = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value);

            var output = this.Get_TypeIdentityName(typeIdentityNameValue);
            return output;
        }

        public IInternalIdentityName Get_TypeIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<TypeIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_TypeIdentityName);

            return output;
        }

        public string Get_NamespacedTypeNameValue(TypeIdentityName typeIdentityName)
        {
            var output = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                typeIdentityName.NamespaceName.Value,
                typeIdentityName.TypeName.Value);

            return output;
        }

        public IInternalIdentityName Get_TypeIdentityName(TypeIdentityName typeIdentityName)
        {
            var namespacedTypeName = this.Get_NamespacedTypeNameValue(typeIdentityName);

            var output = Base.Get_IdentityName(
                typeIdentityName.KindMarker.Value,
                namespacedTypeName)
                .ToIdentityName();

            return output;
        }

        public (TypeIdentityName TypeIdentityName, string FieldName) Get_FieldIdentityNameValues(IInternalIdentityName internalIdentityName)
        {
            // The value of the error identity name is the identity name message.
            var identityNameValue = Base.Get_IdentityNameValue_Unchecked(
                internalIdentityName.Value);

            // For events, the identity name value is the event name separated using the namespace token separator from the type identity name.
            var namespacedTypedFieldName = identityNameValue;

            // Treat the namespaced type name as the namespace name of the event.
            var namespacedTypeName = Instances.NamespacedTypeNameOperator.Get_NamespaceName(namespacedTypedFieldName);

            // Treat the type name as the event name.
            var fieldName = Instances.NamespacedTypeNameOperator.Get_TypeName(namespacedTypedFieldName);

            var typeIdentityName = this.Get_TypeIdentityName(namespacedTypeName);

            return (typeIdentityName, fieldName);
        }

        public IInternalIdentityName Get_FieldIdentityName(
            TypeIdentityName typeIdentityName,
            string fieldName,
            IKindMarker kindMarker)
        {
            var namespacedTypeName = this.Get_NamespacedTypeNameValue(
                typeIdentityName);

            // Treat the type identity name as the namespace name.
            var eventIdentityNameValue = Instances.NamespacedTypeNameOperator.Get_NamespacedTypeName(
                namespacedTypeName,
                fieldName);

            var output = Base.Get_IdentityName(
                kindMarker.Value,
                eventIdentityNameValue)
                .ToIdentityName();

            return output;
        }

        public EventIdentityName Get_EventIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Event);

            var values = this.Get_FieldIdentityNameValues(internalIdentityName);

            var eventName = values.FieldName.ToEventName();

            var output = new EventIdentityName()
            {
                TypeIdentityName = values.TypeIdentityName,
                EventName = eventName,
            };

            return output;
        }

        public IInternalIdentityName Get_EventIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<EventIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_EventIdentityName);

            return output;
        }

        public IInternalIdentityName Get_EventIdentityName(EventIdentityName eventIdentityName)
        {
            var output = this.Get_FieldIdentityName(
                eventIdentityName.TypeIdentityName,
                eventIdentityName.EventName.Value,
                eventIdentityName.KindMarker);

            return output;
        }

        public FieldIdentityName Get_FieldIdentityName(IInternalIdentityName internalIdentityName)
        {
            Instances.IdentityNameOperator_N002.Verify_IsKindMarker(
                internalIdentityName,
                Instances.KindMarkers.Field);

            var values = this.Get_FieldIdentityNameValues(internalIdentityName);

            var fieldName = values.FieldName.ToFieldName();

            var output = new FieldIdentityName()
            {
                TypeIdentityName = values.TypeIdentityName,
                FieldName = fieldName,
            };

            return output;
        }

        public IInternalIdentityName Get_FieldIdentityName(IdentityName identityName)
        {
            var output = Instances.TypeOperator.As_Type_Verify<FieldIdentityName, IdentityName, IInternalIdentityName>(
                identityName,
                this.Get_FieldIdentityName);

            return output;
        }

        public IInternalIdentityName Get_FieldIdentityName(FieldIdentityName eventIdentityName)
        {
            var output = this.Get_FieldIdentityName(
                eventIdentityName.TypeIdentityName,
                eventIdentityName.FieldName.Value,
                eventIdentityName.KindMarker);

            return output;
        }
    }
}
