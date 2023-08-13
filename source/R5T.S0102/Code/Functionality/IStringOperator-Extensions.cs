using System;

using R5T.T0132;


namespace R5T.S0102.N002.Extensions
{
    [FunctionalityMarker]
    public partial interface IStringOperator : IFunctionalityMarker
    {
        /// <inheritdoc cref="IArgument"/>
        public IArgument ToArgument(string value)
        {
            var output = new Argument(value);
            return output;
        }

        /// <inheritdoc cref="IArgumentList"/>
        public IArgumentList ToArgumentList(string value)
        {
            var output = new ArgumentList(value);
            return output;
        }

        /// <inheritdoc cref="IErrorMessage"/>
        public IErrorMessage ToErrorMessage(string value)
        {
            var output = new ErrorMessage(value);
            return output;
        }

        /// <inheritdoc cref="IEventName"/>
        public IEventName ToEventName(string value)
        {
            var output = new EventName(value);
            return output;
        }

        /// <inheritdoc cref="IFieldName"/>
        public IFieldName ToFieldName(string value)
        {
            var output = new FieldName(value);
            return output;
        }

        /// <inheritdoc cref="IIdentityName"/>
        public IIdentityName ToIdentityName(string value)
        {
            var output = new IdentityName(value);
            return output;
        }

        /// <inheritdoc cref="IMethodName"/>
        public IMethodName ToMethodName(string value)
        {
            var output = new MethodName(value);
            return output;
        }

        /// <inheritdoc cref="INamespaceName"/>
        public INamespaceName ToNamespaceName(string value)
        {
            var output = new NamespaceName(value);
            return output;
        }

        /// <inheritdoc cref="INamespacedTypeName"/>
        public INamespacedTypeName ToNamespacedTypeName(string value)
        {
            var output = new NamespacedTypeName(value);
            return output;
        }

        /// <inheritdoc cref="IPropertyName"/>
        public IPropertyName ToPropertyName(string value)
        {
            var output = new PropertyName(value);
            return output;
        }

        /// <inheritdoc cref="ITypeName"/>
        public ITypeName ToTypeName(string value)
        {
            var output = new TypeName(value);
            return output;
        }
    }
}
