using System;


namespace R5T.S0102.N002.Extensions
{
    public static class StringExtensions
    {
        /// <inheritdoc cref="IStringOperator.ToArgument(string)"/>
        public static IArgument ToArgument(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToArgument(value);
        }

        /// <inheritdoc cref="IStringOperator.ToArgumentList(string)"/>
        public static IArgumentList ToArgumentList(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToArgumentList(value);
        }

        /// <inheritdoc cref="IStringOperator.ToErrorMessage(string)"/>
        public static IErrorMessage ToErrorMessage(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToErrorMessage(value);
        }

        /// <inheritdoc cref="IStringOperator.ToEventName(string)"/>
        public static IEventName ToEventName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToEventName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToFieldName(string)"/>
        public static IFieldName ToFieldName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToFieldName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToIdentityName(string)"/>
        public static IIdentityName ToIdentityName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToIdentityName(value);
        }
        
        /// <summary>
        /// Helps when <see cref="ToIdentityName(string)"/> is ambiguous in a certain context.
        /// </summary>
        public static IIdentityName ToInternalIdentityName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToIdentityName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToMethodName(string)"/>
        public static IMethodName ToMethodName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToMethodName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToNamespaceName(string)"/>
        public static INamespaceName ToNamespaceName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToNamespaceName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToNamespacedTypeName(string)"/>
        public static INamespacedTypeName ToNamespacedTypeName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToNamespacedTypeName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToNamespacedTypedName(string)"/>
        public static INamespacedTypedName ToNamespacedTypedName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToNamespacedTypedName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToNamespacedTypedParameterTypedName(string)"/>
        public static INamespacedTypedParameterTypedName ToNamespacedTypedParameterTypedName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToNamespacedTypedParameterTypedName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToPropertyName(string)"/>
        public static IPropertyName ToPropertyName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToPropertyName(value);
        }

        /// <inheritdoc cref="IStringOperator.ToTypeName(string)"/>
        public static ITypeName ToTypeName(this string value)
        {
            return Instances.StringOperator_N002_Extensions.ToTypeName(value);
        }
    }
}
