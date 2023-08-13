using System;
using System.Collections.Generic;
using System.Text;

using R5T.F0000;
using R5T.F0000.Extensions;
using R5T.T0132;


namespace R5T.S0102.Internal
{
    /// <summary>
    /// Works at the string level of abstraction.
    /// </summary>
    [FunctionalityMarker]
    public partial interface IIdentityNameOperator : IFunctionalityMarker
    {
        private static T0162.F001.IIdentityNameOperator Base => T0162.F001.IdentityNameOperator.Instance;


        public string[] Get_Arguments(string argumentsList)
        {
            if(Instances.StringOperator.IsNullOrEmpty(argumentsList))
            {
                return Instances.ArrayOperator.New_Empty<string>();
            }

            // The difficulty is that the argument separator ',' (comma) is used recursively to separate arguments within the arguments list, and type arguments within the generic types of arguments.

            var tokens = Instances.StringOperator.Split(
                Instances.TokenSeparators.ArgumentListSeparator,
                argumentsList);

            var openBraceCount = 0;
            var closeBraceCount = 0;

            var builder = new StringBuilder();

            var arguments = new List<string>();

            foreach (var token in tokens)
            {
                var inTypeArgumentList = openBraceCount > closeBraceCount;

                var appendix = inTypeArgumentList
                    // Inside argument type argument lists, we want to keep the argument list separator (comma).
                    ? $"{Instances.TokenSeparators.ArgumentListSeparator}{token}"
                    // Else, just the token.
                    : token
                    ;

                builder.Append(appendix);

                var currentOpenBraceCount = Instances.StringOperator.CountOf(
                    Instances.TokenSeparators.TypeArgumentListOpenSeparator,
                    token);

                var currentCloseBraceCount = Instances.StringOperator.CountOf(
                    Instances.TokenSeparators.TypeArgumentListCloseSeparator,
                    token);

                openBraceCount += currentOpenBraceCount;
                closeBraceCount += currentCloseBraceCount;

                if(openBraceCount == closeBraceCount)
                {
                    var argument = builder.ToString();

                    arguments.Add(argument);

                    builder.Clear();

                    openBraceCount = 0;
                    closeBraceCount = 0;
                }
            }

            // Be strict, if there is a remaining open-close brace count imbalance, say so.
            if(openBraceCount != 0 || closeBraceCount != 0)
            {
                throw new Exception($"Open-close brace count imbalance ({openBraceCount} open, {closeBraceCount} close).");
            }

            return arguments.ToArray();
        }

        public WasFound<string> Has_OutputType(string methodIdentityName)
        {
            var indexOfOutputTypeSeparatorFound = Instances.StringOperator.IndexOf(
                Instances.TokenSeparators.OutputTypeTokenSeparator,
                methodIdentityName);

            if(indexOfOutputTypeSeparatorFound)
            {
                var outputTypeName = Instances.StringOperator.Get_Substring_From_Exclusive(
                    indexOfOutputTypeSeparatorFound,
                    methodIdentityName);

                return WasFound.Found(outputTypeName);
            }
            else
            {
                return WasFound.NotFound<string>();
            }
        }

        public bool Is_GenericTypeName(string identityName)
        {
            // If the identity name contains the type parameter count separator ('`', back-tick), then the identity name is generic.
            var output = Instances.StringOperator.Contains(
                identityName,
                Instances.TokenSeparators.TypeParameterCountSeparator);

            return output;
        }

        public bool Is_GenericMethodName(string methodIdentityName)
        {
            // If the identity name contains the method type parameter count separator ('``', two back-ticks), then the method identity name is generic.
            var output = Instances.StringOperator.Contains(
                methodIdentityName,
                Instances.TokenSeparators.MethodTypeParameterCountSeparator);

            return output;
        }

        public bool Is_EventIdentityName(string identityName)
        {
            var kindMarker = Base.Get_KindMarker_Unchecked(identityName);

            var output = kindMarker == Instances.KindMarkers.Event.Value;
            return output;
        }

        public bool Is_FieldIdentityName(string identityName)
        {
            var kindMarker = Base.Get_KindMarker_Unchecked(identityName);

            var output = kindMarker == Instances.KindMarkers.Field.Value;
            return output;
        }

        public bool Is_MethodIdentityName(string identityName)
        {
            var kindMarker = Base.Get_KindMarker_Unchecked(identityName);

            var output = kindMarker == Instances.KindMarkers.Method.Value;
            return output;
        }

        public bool Is_OutputTyped(string methodIdentityName)
        {
            var output = Instances.StringOperator.Contains(
                methodIdentityName,
                Instances.TokenSeparators.OutputTypeTokenSeparator);

            return output;
        }

        public bool Is_PropertyIdentityName(string identityName)
        {
            var kindMarker = Base.Get_KindMarker_Unchecked(identityName);

            var output = kindMarker == Instances.KindMarkers.Property.Value;
            return output;
        }

        public string Get_ArgumentListValue(string methodIdentityNameValue)
        {
            var indexOfOpenParenthesisFound = Instances.StringOperator.IndexOf(
                Instances.TokenSeparators.ParameterListOpenTokenSeparator,
                methodIdentityNameValue);

            if (!indexOfOpenParenthesisFound)
            {
                return Instances.Strings.Empty;
            }

            var indexOfCloseParenthesisFound = Instances.StringOperator.IndexOf(
                Instances.TokenSeparators.ParameterListCloseTokenSeparator,
                methodIdentityNameValue,
                indexOfOpenParenthesisFound);

            var output = indexOfCloseParenthesisFound
                ? Instances.StringOperator.Get_Substring_Exclusive_Exclusive(
                    indexOfOpenParenthesisFound,
                    indexOfCloseParenthesisFound,
                    methodIdentityNameValue)
                : Instances.StringOperator.Get_Substring_From_Exclusive(
                    indexOfOpenParenthesisFound,
                    methodIdentityNameValue)
                ;

            return output;
        }

        public bool Is_TypeIdentityName(string identityName)
        {
            var kindMarker = Base.Get_KindMarker_Unchecked(identityName);

            var output = kindMarker == Instances.KindMarkers.Type.Value;
            return output;
        }
    }
}
